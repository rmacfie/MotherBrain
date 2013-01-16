namespace MotherBrain.Tests
{
    using System;

    public interface IService
    {
    }

    public class AService : IService
    {
    }

    public class AService2 : IService
    {
    }

	public class AService3 : IService
	{
	}

	public class AService4 : IService
	{
	}

    public interface IDisposableService : IDisposable
    {
        bool IsDisposed { get; }
    }

    public class ADisposableService : IDisposableService
    {
        public void Dispose()
        {
            IsDisposed = true;
        }

        public bool IsDisposed { get; private set; }
    }

    public interface IComposedService
    {
    }

    public class AComposedService : IComposedService
    {
        readonly IService service;

        public AComposedService(IService service)
        {
            this.service = service;
        }
    }

    public interface IComposedService2
    {
    }

    public class AComposedService2 : IComposedService2
    {
        readonly IComposedService composedService;
        readonly IService service;

        public AComposedService2(IService service, IComposedService composedService)
        {
            this.service = service;
            this.composedService = composedService;
        }
    }
}