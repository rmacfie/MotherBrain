namespace MotherBrain
{
    using System;
    using System.Collections.Concurrent;

    public class Container : IContainer
    {
        readonly ConcurrentDictionary<Type, IProvider> providers = new ConcurrentDictionary<Type, IProvider>();

        public T Resolve<T>()
        {
            var type = typeof(T);
            IProvider provider;

            if (!providers.TryGetValue(type, out provider))
                throw new ResolvanceException(string.Format("Unknown type ({0}).", type.FullName));

            return (T)provider.GetInstance(this);
        }

        public void Register<TConcrete, T>(Func<IContainer, TConcrete> factory) where TConcrete : T
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            var type = typeof(T);

            if (providers.ContainsKey(type))
                throw new RegistrationException(string.Format("The type T ({0}) is already registered.", type.FullName));

            providers[type] = new TransientProvider<TConcrete>(factory);
        }
    }
}