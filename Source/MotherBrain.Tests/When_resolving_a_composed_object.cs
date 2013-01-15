namespace MotherBrain.Tests
{
    using Machine.Specifications;

	public class When_resolving_a_composed_object : With_container
    {
        static IComposedService instance;

        Establish context = () =>
        {
            container.RegisterTransient<IService>(c => new AService());
            container.RegisterTransient<IComposedService>(c => new AComposedService(c.Get<IService>()));
        };

        Because of = () =>
            instance = container.Get<IComposedService>();

        It should_return_instance = () =>
            instance.ShouldNotBeNull();
    }
}