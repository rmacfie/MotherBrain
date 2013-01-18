namespace MotherBrain
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;

	public class FactoryBuilder
	{
		readonly Func<object[], object> factory;
		readonly Type[] parameters;

		public FactoryBuilder(Type type)
		{
			var ctor = type.GetConstructors().OrderBy(x => x.GetParameters().Count()).LastOrDefault();

			if (ctor == null)
				throw new MotherBrainException(string.Format("Couldn't find a public constructor on '{0}'.", type.FullName));

			parameters = ctor.GetParameters().Select(x => x.ParameterType).ToArray();

			var param = Expression.Parameter(typeof(object[]), "args");
			var argsExp = new Expression[parameters.Length];

			for (var i = 0; i < parameters.Length; i++)
			{
				var index = Expression.Constant(i);
				var accessorExp = Expression.ArrayIndex(param, index);
				var castExp = Expression.Convert(accessorExp, parameters[i]);

				argsExp[i] = castExp;
			}

			var newExp = Expression.New(ctor, argsExp);
			var lambda = Expression.Lambda(typeof(FactoryDelegate), newExp, param);
			var compiled = (FactoryDelegate)lambda.Compile();

			factory = args => compiled(args);
		}

		public Func<IContainer, TOut> BuildFactory<TOut>()
		{
			return container =>
			{
				var dependencies = parameters.Select(container.Get).ToArray();
				return (TOut)factory.Invoke(dependencies);
			};
		}

		delegate object FactoryDelegate(object[] args);
	}
}