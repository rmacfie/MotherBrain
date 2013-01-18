namespace MotherBrain.Providers
{
    using System;

    public class TransientProvider<T> : Provider
    {
        readonly Func<IContainer, T> factory;

        public TransientProvider(Key key, Func<IContainer, T> factory) : base(key)
        {
            this.factory = factory;
        }

	    public override object GetInstance(IContainer container)
        {
            return factory.Invoke(container);
        }
    }
}