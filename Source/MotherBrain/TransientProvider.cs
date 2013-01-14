namespace MotherBrain
{
    using System;

    public class TransientProvider<TConcrete> : IProvider
    {
        readonly Func<IContainer, TConcrete> factory;

        public TransientProvider(Func<IContainer, TConcrete> factory)
        {
            this.factory = factory;
        }

        public object GetInstance(IContainer container)
        {
            return factory.Invoke(container);
        }
    }
}