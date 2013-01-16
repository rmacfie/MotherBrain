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
        /// Registers a factory that will be used when asking for T. A single instance will be created and
        /// managed per container. This instance will be disposed (if applicable) when the container is disposed.
        /// </summary>
        public static void RegisterSingleton<T>(this IContainer container, Func<IContainer, T> factory)
        {
            container.RegisterSingletonPerContainer(factory, null);
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