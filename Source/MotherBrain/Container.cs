namespace MotherBrain
{
    using System;

    public class Container : IContainer
    {
        public T Resolve<T>()
        {
            throw new NotImplementedException();
        }

        public void Register<TImpl, T>(Func<IContainer, TImpl> factory) where TImpl : T
        {
            throw new NotImplementedException();
        }
    }
}