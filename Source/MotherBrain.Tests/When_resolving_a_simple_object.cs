namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public class When_resolving_a_simple_object
    {
        static IContainer container;
        static IService instance;

        Establish context = () =>
        {
            container = new Container();
            container.RegisterTransient<AService, IService>(c => new AService());
        };

        Because of = () =>
            instance = container.Get<IService>();

        It should_return_instance = () =>
            instance.ShouldNotBeNull();
    }
}