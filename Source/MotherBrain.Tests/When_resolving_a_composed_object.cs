namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public class When_resolving_a_composed_object
    {
        static IContainer container;
        static IComposedService instance;

        Establish context = () =>
        {
            container = new Container();
            container.RegisterTransient<AService, IService>(c => new AService());
            container.RegisterTransient<AComposedService, IComposedService>(c => new AComposedService(c.Get<IService>()));
        };

        Because of = () =>
            instance = container.Get<IComposedService>();

        It should_return_instance = () =>
            instance.ShouldNotBeNull();
    }
}