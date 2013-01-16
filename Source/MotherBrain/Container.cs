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

        public object Get(Type type)
        {
            return Get(type, null);
        }

		public object Get(Type type, string name)
        {
            var key = new Key(type, name);
            IProvider provider;

            if (!providers.TryGetValue(key, out provider))
                throw new ResolutionException(string.Format("Couldn't find any registrations with the key ({0}).", key));

            return provider.GetInstance(this);
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T), null);
        }

        public T Get<T>(string name)
        {
			return (T)Get(typeof(T), name);
        }

        public void RegisterConstant<T>(T instance)
        {
            RegisterConstant(instance, null);
        }

        public void RegisterConstant<T>(T instance, string name)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            var key = new Key(typeof(T), name);
            Register(new ConstantProvider<T>(key, instance));
        }

        public void RegisterSingleton<T>(Func<IContainer, T> factory)
        {
            RegisterSingleton(factory, null);
        }

        public void RegisterSingleton<T>(Func<IContainer, T> factory, string name)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            var key = new Key(typeof(T), name);
            Register(new SingletonProvider<T>(key, factory));
        }

        public void RegisterTransient<T>(Func<IContainer, T> factory)
        {
            RegisterTransient(factory, null);
        }

        public void RegisterTransient<T>(Func<IContainer, T> factory, string name)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            var key = new Key(typeof(T), name);
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