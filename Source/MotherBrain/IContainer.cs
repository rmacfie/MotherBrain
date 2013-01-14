namespace MotherBrain
{
    using System;

    public interface IContainer
    {
        T Resolve<T>();

        void Register<TImpl, T>(Func<IContainer, TImpl> factory) where TImpl : T;
    }
}