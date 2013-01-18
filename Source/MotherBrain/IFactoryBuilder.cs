namespace MotherBrain
{
    using System;

    /// <summary>
    /// Creates a factory for dynamically activating instances based only on the type.
    /// </summary>
    public interface IFactoryBuilder
    {
        Func<IContainer, TOut> BuildFactory<TOut>();
    }
}