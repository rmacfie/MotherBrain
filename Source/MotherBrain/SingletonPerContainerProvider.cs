namespace MotherBrain
{
    using System;

    public class SingletonPerContainerProvider<T> : Provider
    {
        readonly Func<IContainer, T> factory;

        public SingletonPerContainerProvider(Key key, Func<IContainer, T> factory) : base(key)
        {
            this.factory = factory;
        }

        public override object GetInstance(IContainer container)
        {
            return container.Store.GetOrAdd(Key, x => factory.Invoke(container));
        }
    }
}