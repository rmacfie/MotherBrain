namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public class When_resolving_a_concrete_registered_as_itself : With_container
    {
        static IService instance;

        Establish context = () =>
            container.RegisterTransient(c => new AService());

        Because of = () =>
            instance = container.Get<AService>();

        It should_return_instance = () =>
            instance.ShouldNotBeNull();
    }
}