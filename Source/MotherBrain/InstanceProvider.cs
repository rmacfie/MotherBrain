namespace MotherBrain
{
    public class InstanceProvider<TConcrete> : IProvider
    {
        readonly TConcrete instance;

        public InstanceProvider(TConcrete instance)
        {
            this.instance = instance;
        }

        public object GetInstance(IContainer container)
        {
            return instance;
        }
    }
}