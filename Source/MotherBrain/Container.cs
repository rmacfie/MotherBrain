namespace MotherBrain
{
    using System;
    using System.Collections.Concurrent;

    public class Container : IContainer
    {
        readonly ConcurrentDictionary<Type, IProvider> providers = new ConcurrentDictionary<Type, IProvider>();

        public T Get<T>()
        {
            var type = typeof(T);
            IProvider provider;

            if (!providers.TryGetValue(type, out provider))
                throw new ResolvanceException(string.Format("Unknown type ({0}).", type.FullName));

            return (T)provider.GetInstance(this);
        }

        public void RegisterTransient<TConcrete, T>(Func<IContainer, TConcrete> factory) where TConcrete : T
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            Register<T>(new TransientProvider<TConcrete>(factory));
        }

        public void RegisterInstance<TConcrete, T>(TConcrete instance) where TConcrete : T
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            Register<T>(new InstanceProvider<TConcrete>(instance));
        }

        void Register<T>(IProvider provider)
        {
            var type = typeof(T);

            if (!providers.TryAdd(type, provider))
                throw new RegistrationException(string.Format("The type T ({0}) is already registered.", type.FullName));
        }
    }
}