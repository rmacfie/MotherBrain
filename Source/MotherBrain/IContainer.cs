using System.Collections.Generic;

namespace MotherBrain
{
    using System;

    public interface IContainer : IDisposable
    {
        /// <summary>
        /// Extension point for providers.
        /// </summary>
        InstanceStore ManagedInstances { get; }

		/// <summary>
		/// Resolves an instance of the given type.
		/// </summary>
		object Get(Type type);

        /// <summary>
        /// Resolves an instance of the given type.
        /// </summary>
		T Get<T>();

        /// <summary>
        /// Resolves an instance of the given type with the given name.
        /// </summary>
        object Get(Type type, string name);

        /// <summary>
        /// Resolves an instance of the given type with the given name.
        /// </summary>
        T Get<T>(string name);

		/// <summary>
		/// Resolves all instances of the given type, named and unnamed.
		/// </summary>
		IEnumerable<object> GetAll(Type type);

		/// <summary>
		/// Resolves all instances of the given type, named and unnamed.
		/// </summary>
		IEnumerable<T> GetAll<T>();

        /// <summary>
        ///  Registers an instance that will be used when asking for T.
        /// </summary>
        void RegisterConstant<T>(T instance);

        /// <summary>
        ///  Registers an instance that will be used when asking for T and the given name.
        /// </summary>
        void RegisterConstant<T>(T instance, string name);

        /// <summary>
        /// Registers a factory that will be used when asking for T. A single instance will be created and
        /// managed per container. This instance will be disposed (if applicable) when the container is disposed.
        /// </summary>
        void RegisterSingleton<T>(Func<IContainer, T> factory);

        /// <summary>
        /// Registers a factory that will be used when asking for T and the given name. A single instance will be
        /// created and managed per container. This instance will be disposed (if applicable) when the container
        /// is disposed.
        /// </summary>
        void RegisterSingleton<T>(Func<IContainer, T> factory, string name);

        /// <summary>
        /// Registers a factory that will be used when asking for T. A new instance will be created every time.
        /// </summary>
        void RegisterTransient<T>(Func<IContainer, T> factory);

        /// <summary>
        /// Registers a factory that will be used when asking for T and the given name. A new instance will be
        /// created every time.
        /// </summary>
        void RegisterTransient<T>(Func<IContainer, T> factory, string name);

        /// <summary>
        /// Extension point for custom providers.
        /// </summary>
        void Register(IProvider provider);
    }
}