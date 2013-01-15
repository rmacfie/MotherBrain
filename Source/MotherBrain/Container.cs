using System;
using System.Collections.Concurrent;

namespace MotherBrain
{
	public class Container : IContainer
	{
		readonly ConcurrentDictionary<Key, IProvider> providers = new ConcurrentDictionary<Key, IProvider>();
		readonly InstanceStore managedInstances = new InstanceStore();

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

			Register<T>(new ConstantProvider<T>(instance));
		}

		public void RegisterSingleton<T>(Func<IContainer, T> factory)
		{
			if (factory == null)
				throw new ArgumentNullException("factory");

			Register<T>(new SingletonProvider<T>(factory));
		}

		public void RegisterTransient<T>(Func<IContainer, T> factory)
		{
			if (factory == null)
				throw new ArgumentNullException("factory");

			Register<T>(new TransientProvider<T>(factory));
		}

		public void Register<T>(IProvider provider)
		{
			var key = new Key(typeof(T));

			if (!providers.TryAdd(key, provider))
				throw new RegistrationException(string.Format("The type T ({0}) is already registered.", key.Type.FullName));
		}

		//public void Manage(Guid id, IDisposable instance)
		//{
		//	if (!managedInstances.TryAdd(id, instance))
		//		throw new MotherBrainException(string.Format("This container already manages an instance with the id: {0}.", id));
		//}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				//while (!managedInstances.IsEmpty)
				//{
				//	IDisposable instance;

				//	//if (managedInstances.TryTake(out instance))
				//	//	instance.Dispose();
				//}
			}
		}
	}
}