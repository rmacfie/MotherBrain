namespace MotherBrain
{
    using System;
	using System.Collections.Generic;
	using Providers;

    public interface IContainer : IDisposable
    {
        /// <summary>
        /// Extension point for providers.
        /// </summary>
        InstanceStore Store { get; }

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
        ///  Registers an instance that will be used when asking for T and the given name.
        /// </summary>
        void RegisterConstant<T>(T instance, string name);

        /// <summary>
        /// Registers a type TImpl that will be used when asking for T and the given name. A single instance will be
        /// created per container.
        /// </summary>
        void RegisterSingletonPerContainer<T, TImpl>(string name);

        /// <summary>
        /// Registers a factory that will be used when asking for T and the given name. A single instance will be
        /// created per container.
        /// </summary>
        void RegisterSingletonPerContainer<T>(Func<IContainer, T> factory, string name);

        /// <summary>
        /// Registers a type TImpl that will be used when asking for T and the given name. A single instance will be
        /// created per HttpContext (if available) or thread.
        /// </summary>
        void RegisterSingletonPerContext<T, TImpl>(string name);

        /// <summary>
        /// Registers a factory that will be used when asking for T and the given name. A single instance will be
        /// created per HttpContext (if available) or Thread.
        /// </summary>
        void RegisterSingletonPerContext<T>(Func<IContainer, T> factory, string name);

        /// <summary>
        /// Registers a type TImpl that will be used when asking for T and the given name. A new instance will be
        /// created every time.
        /// </summary>
        void RegisterTransient<T, TImpl>(string name);

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