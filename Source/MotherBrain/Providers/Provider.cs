namespace MotherBrain.Providers
{
    public abstract class Provider : IProvider
    {
        readonly Key key;

        protected Provider(Key key)
        {
            this.key = key;
        }

        public Key Key
        {
            get { return key; }
        }

        public abstract object GetInstance(IContainer container);
    }
}