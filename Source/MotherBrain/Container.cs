namespace MotherBrain
{
    using System;
    using System.Collections.Concurrent;

    public class Container : IContainer
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
                throw new ResolutionException(string.Format("Unknown type ({0}).", key.Type.FullName));

            return (T)provider.GetInstance(this);
        }

        public void RegisterConstant<T>(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            var key = new Key(typeof(T));
            Register(key, new ConstantProvider<T>(key, instance));
        }

        public void RegisterSingleton<T>(Func<IContainer, T> factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            var key = new Key(typeof(T));
            Register(key, new SingletonProvider<T>(key, factory));
        }

        public void RegisterTransient<T>(Func<IContainer, T> factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            var key = new Key(typeof(T));
            Register(key, new TransientProvider<T>(key, factory));
        }

        public void Register(Key key, IProvider provider)
        {
            if (!providers.TryAdd(key, provider))
                throw new RegistrationException(string.Format("The type T ({0}) is already registered.", key.Type.FullName));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ManagedInstances.Dispose();
            }
        }
    }
}