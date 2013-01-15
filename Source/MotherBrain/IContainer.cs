using System;

namespace MotherBrain
{
	public interface IContainer : IDisposable
	{
		/// <summary>
		/// Resolves an instance of the given type.
		/// </summary>
		T Get<T>();

		/// <summary>
		/// Registers an instance that will be used when asking for T.
		/// </summary>
		void RegisterConstant<T>(T instance);

		/// <summary>
		/// Registers a factory that will be used when asking for T. Once
		/// created, the same instance will live as a singleton throughout
		/// the container's lifetime and will be disposed when the
		/// container is disposed.
		/// </summary>
		void RegisterSingleton<T>(Func<IContainer, T> factory);

		/// <summary>
		/// Registers a factory that will be used when asking for T.
		/// A new instance will be created every time.
		/// </summary>
		void RegisterTransient<T>(Func<IContainer, T> factory);

		/// <summary>
		/// Extension point for custom providers.
		/// </summary>
		void Register<T>(IProvider provider);
	}
}