namespace MotherBrain
{
    using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;


    public sealed class Container : IContainer
    {
        readonly ConcurrentDictionary<Key, IProvider> providers = new ConcurrentDictionary<Key, IProvider>();
        readonly InstanceStore store = new InstanceStore();

        public InstanceStore Store
        {
            get { return store; }
        }

		public object Get(Type type, string name)
        {
            var key = new Key(type, name);
            IProvider provider;

            if (!providers.TryGetValue(key, out provider))
                throw new ResolutionException(string.Format("Couldn't find any registrations with the key ({0}).", key));

            return provider.GetInstance(this);
        }

        public T Get<T>(string name)
        {
			return (T)Get(typeof(T), name);
        }

	    public IEnumerable<object> GetAll(Type type)
	    {
		    return providers.Where(x => x.Key.Type == type).Select(x => x.Value.GetInstance(this)).ToArray();
	    }

	    public IEnumerable<T> GetAll<T>()
	    {
		    return GetAll(typeof(T)).Cast<T>();
	    }

        public void RegisterConstant<T>(T instance, string name)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            var key = new Key(typeof(T), name);
            Register(new ConstantProvider<T>(key, instance));
        }

        public void RegisterSingleton<T>(Func<IContainer, T> factory, string name)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            var key = new Key(typeof(T), name);
            Register(new SingletonProvider<T>(key, factory));
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
            Store.Dispose();
        }
    }
}