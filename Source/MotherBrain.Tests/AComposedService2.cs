namespace MotherBrain.Tests
{
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