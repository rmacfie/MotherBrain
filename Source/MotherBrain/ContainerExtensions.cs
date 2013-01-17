namespace MotherBrain
{
    using System;

    public static class ContainerExtensions
    {
        /// <summary>
        /// Resolves an instance of the given type.
        /// </summary>
        public static object Get(this IContainer container, Type type)
        {
            return container.Get(type, null);
        }

        /// <summary>
        /// Resolves an instance of the given type.
        /// </summary>
        public static T Get<T>(this IContainer container)
        {
            return (T)container.Get(typeof(T), null);
        }

        /// <summary>
        ///  Registers an instance that will be used when asking for T.
        /// </summary>
        public static void RegisterConstant<T>(this IContainer container, T instance)
        {
            container.RegisterConstant(instance, null);
        }

        /// <summary>
        /// Registers a type TImpl that will be used when asking for T and the given name. A single instance will be
        /// created per container.
        /// </summary>
        public static void RegisterSingletonPerContainer<T, TImpl>(this IContainer container, string name)
        {
            container.RegisterSingletonPerContainer<T, TImpl>(null);
        }

        /// <summary>
        /// Registers a factory that will be used when asking for T and the given name. A single instance will be
        /// created per container.
        /// </summary>
        public static void RegisterSingletonPerContainer<T>(this IContainer container, Func<IContainer, T> factory)
        {
            container.RegisterSingletonPerContainer(factory, null);
        }

        /// <summary>
        /// Registers a type TImpl that will be used when asking for T and the given name. A single instance will be
        /// created per HttpContext (if available) or thread.
        /// </summary>
        public static void RegisterSingletonPerContext<T, TImpl>(this IContainer container, string name)
        {
            container.RegisterSingletonPerContext<T, TImpl>(null);
        }

        /// <summary>
        /// Registers a factory that will be used when asking for T and the given name. A single instance will be
        /// created per HttpContext (if available) or Thread.
        /// </summary>
        public static void RegisterSingletonPerContext<T>(this IContainer container, Func<IContainer, T> factory)
        {
            container.RegisterSingletonPerContext(factory, null);
        }

        /// <summary>
        /// Registers a type TImpl that will be used when asking for T and the given name. A new instance will be
        /// created every time.
        /// </summary>
        public static void RegisterTransient<T, TImpl>(this IContainer container, string name)
        {
            container.RegisterTransient<T, TImpl>(null);
        }

        /// <summary>
        /// Registers a factory that will be used when asking for T. A new instance will be created every time.
        /// </summary>
        public static void RegisterTransient<T>(this IContainer container, Func<IContainer, T> factory)
        {
            container.RegisterTransient(factory, null);
        }
    }
}