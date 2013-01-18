namespace MotherBrain.Providers
{
    public class ConstantProvider<T> : Provider
    {
        readonly T instance;

        public ConstantProvider(Key key, T instance) : base(key)
        {
            this.instance = instance;
        }

        public override object GetInstance(IContainer container)
        {
            return instance;
        }
    }
}