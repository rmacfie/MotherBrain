namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public class When_disposing_the_container_with_an_instantiated_singleton
    {
        static IContainer container;
        static IDisposableService instance;

        Establish context = () =>
        {
            container = new Container();
            container.RegisterSingleton<IDisposableService>(c => new ADisposableService());
            instance = container.Get<IDisposableService>();
        };

        Because of = () =>
            container.Dispose();

        It should_dispose_the_singleton_instance = () =>
            instance.IsDisposed.ShouldBeTrue();
    }
}