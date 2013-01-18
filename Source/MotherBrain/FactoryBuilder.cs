namespace MotherBrain
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class FactoryBuilder : IFactoryBuilder
    {
        readonly Func<object[], object> factory;
        readonly Type[] parameterTypes;

        public FactoryBuilder(Type type)
        {
            var ctor = type.GetConstructors().OrderBy(x => x.GetParameters().Count()).LastOrDefault();

            if (ctor == null)
                throw new MotherBrainException(string.Format("Couldn't find a public constructor on '{0}'.", type.FullName));

            parameterTypes = ctor.GetParameters().Select(x => x.ParameterType).ToArray();

            var parameters = Expression.Parameter(typeof(object[]), "args");
            var arguments = new Expression[parameterTypes.Length];

            for (var i = 0; i < parameterTypes.Length; i++)
            {
                var index = Expression.Constant(i);
                var accessor = Expression.ArrayIndex(parameters, index);
                var cast = Expression.Convert(accessor, parameterTypes[i]);

                arguments[i] = cast;
            }

            var newExpression = Expression.New(ctor, arguments);
            var lambda = Expression.Lambda(typeof(FactoryDelegate), newExpression, parameters);
            var compiled = (FactoryDelegate)lambda.Compile();

            factory = args => compiled(args);
        }

        public Func<IContainer, TOut> BuildFactory<TOut>()
        {
            return container =>
            {
                var dependencies = parameterTypes.Select(container.Get).ToArray();
                return (TOut)factory.Invoke(dependencies);
            };
        }

        delegate object FactoryDelegate(object[] args);
    }
}