namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public class When_resolving_a_constant : With_container
    {
        static IService instance;
        static AService registeredInstance;

        Establish context = () =>
        {
            registeredInstance = new AService();
            container.RegisterConstant<IService>(registeredInstance);
        };

        Because of = () =>
            instance = container.Get<IService>();

        It should_return_the_same_instance = () =>
            instance.ShouldBeTheSameAs(registeredInstance);
    }
}