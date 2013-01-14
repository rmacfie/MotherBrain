namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public class When_resolving_an_instance
    {
        static IContainer container;
        static IService instance;

        Establish context = () =>
        {
            container = new Container();
            container.Register<ImplementsIService, IService>(c => new ImplementsIService());
        };

        Because of = () =>
            instance = container.Resolve<IService>();

        It should_return_instance = () =>
            instance.ShouldNotBeNull();
    }

    public interface IService
    {
    }

    public class ImplementsIService : IService
    {
    }
}