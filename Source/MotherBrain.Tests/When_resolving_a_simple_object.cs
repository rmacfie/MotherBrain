namespace MotherBrain.Tests
{
    using Machine.Specifications;

	public class When_resolving_a_simple_object : With_container
    {
        static IService instance;

        Establish context = () =>
			container.RegisterTransient<IService>(c => new AService());

        Because of = () =>
            instance = container.Get<IService>();

        It should_return_instance = () =>
            instance.ShouldNotBeNull();
    }
}