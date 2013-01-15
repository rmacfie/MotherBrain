namespace MotherBrain
{
    using System;
    using System.Collections.Concurrent;

    public sealed class Container : IContainer
    {
        readonly InstanceStore managedInstances = new InstanceStore();
        readonly ConcurrentDictionary<Key, IProvider> providers = new ConcurrentDictionary<Key, IProvider>();

        public InstanceStore ManagedInstances
        {
            get { return managedInstances; }
        }

        public T Get<T>()
        {
            var key = new Key(typeof(T));
            IProvider provider;

            if (!providers.TryGetValue(key, out provider))
                throw new ResolutionException(string.Format("Couldn't find any registrations with the key ({0}).", key));

            return (T)provider.GetInstance(this);
        }

        public void RegisterConstant<T>(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            var key = new Key(typeof(T));
            Register(new ConstantProvider<T>(key, instance));
        }

        public void RegisterSingleton<T>(Func<IContainer, T> factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            var key = new Key(typeof(T));
            Register(new SingletonProvider<T>(key, factory));
        }

        public void RegisterTransient<T>(Func<IContainer, T> factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            var key = new Key(typeof(T));
            Register(new TransientProvider<T>(key, factory));
        }

        public void Register(IProvider provider)
        {
            if (!providers.TryAdd(provider.Key, provider))
                throw new RegistrationException(string.Format("There is already a registration with the key {0}.", provider.Key));
        }

        public void Dispose()
        {
            ManagedInstances.Dispose();
        }
    }
}