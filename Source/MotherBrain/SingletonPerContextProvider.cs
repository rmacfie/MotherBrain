namespace MotherBrain
{
    using System;
    using System.Web;

    public class SingletonPerContextProvider<T> : Provider
    {
        [ThreadStatic]
        static InstanceStore threadStore;

        readonly Func<IContainer, T> factory;
        readonly string id = string.Format("MotherBrain.PerContextStore[{0}]", Guid.NewGuid());

        public SingletonPerContextProvider(Key key, Func<IContainer, T> factory) : base(key)
        {
            this.factory = factory;
        }

        InstanceStore Store
        {
            get
            {
                InstanceStore store;

                if (HttpContext.Current != null)
                {
                    store = (InstanceStore)(HttpContext.Current.Items[id] ?? (HttpContext.Current.Items[id] = new InstanceStore()));
                }
                else
                {
                    store = threadStore ?? (threadStore = new InstanceStore());
                }

                return store;
            }
        }

        public override object GetInstance(IContainer container)
        {
            return Store.GetOrAdd(Key, x => factory.Invoke(container));
        }
    }
}