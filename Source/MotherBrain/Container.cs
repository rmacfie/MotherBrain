namespace MotherBrain
{
    using System;
    using System.Collections.Concurrent;

    public class Container : IContainer
    {
        readonly ConcurrentDictionary<Type, IProvider> providers = new ConcurrentDictionary<Type, IProvider>();

        public T Resolve<T>()
        {
            var type = typeof(T);
            var provider = providers[type];

            return (T)provider.GetInstance(this);
        }

        public void Register<TConcrete, T>(Func<IContainer, TConcrete> factory) where TConcrete : T
        {
            var type = typeof(T);
            providers[type] = new TransientProvider<TConcrete>(factory);
        }
    }
}