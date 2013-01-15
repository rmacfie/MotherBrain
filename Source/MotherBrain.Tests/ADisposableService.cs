namespace MotherBrain.Tests
{
    public class ADisposableService : IDisposableService
    {
        public void Dispose()
        {
            IsDisposed = true;
        }

        public bool IsDisposed { get; private set; }
    }
}