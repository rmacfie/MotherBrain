namespace MotherBrain
{
    using System;

    public class SingletonProvider<T> : Provider
    {
        readonly Func<IContainer, T> factory;

        public SingletonProvider(Key key, Func<IContainer, T> factory) : base(key)
        {
            this.factory = factory;
        }

        public override object GetInstance(IContainer container)
        {
            return container.ManagedInstances.GetOrAdd(Key, x => factory.Invoke(container));
        }
    }
}