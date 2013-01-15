namespace MotherBrain.Tests
{
    using System;

    public interface IDisposableService : IDisposable
    {
        bool IsDisposed { get; }
    }
}