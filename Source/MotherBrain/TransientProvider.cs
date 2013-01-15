namespace MotherBrain
{
    using System;

    public class TransientProvider<T> : IProvider
    {
        readonly Func<IContainer, T> factory;

        public TransientProvider(Func<IContainer, T> factory)
        {
            this.factory = factory;
        }

        public object GetInstance(IContainer container)
        {
            return factory.Invoke(container);
        }
    }
}