namespace MotherBrain.Tests
{
    public class AComposedService : IComposedService
    {
        readonly IService service;

        public AComposedService(IService service)
        {
            this.service = service;
        }
    }
}