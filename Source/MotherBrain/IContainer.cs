namespace MotherBrain
{
    using System;

    public interface IContainer
    {
        T Get<T>();

        void RegisterTransient<TConcrete, T>(Func<IContainer, TConcrete> factory) where TConcrete : T;

        void RegisterInstance<TConcrete, T>(TConcrete instance) where TConcrete : T;
    }
}