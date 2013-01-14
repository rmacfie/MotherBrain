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
            container.Register<AService, IService>(c => new AService());
        };

        Because of = () =>
            instance = container.Resolve<IService>();

        It should_return_instance = () =>
            instance.ShouldNotBeNull();
    }
}