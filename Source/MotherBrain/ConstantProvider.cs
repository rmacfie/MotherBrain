namespace MotherBrain
{
    public class ConstantProvider<T> : IProvider
    {
        readonly T instance;

        public ConstantProvider(T instance)
        {
            this.instance = instance;
        }

        public object GetInstance(IContainer container)
        {
            return instance;
        }
    }
}