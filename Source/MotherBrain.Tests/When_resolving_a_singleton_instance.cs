namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public class When_resolving_a_singleton_instance
    {
        static IContainer container;
        static IService instance;
        static AService registeredInstance;

        Establish context = () =>
        {
            container = new Container();
            registeredInstance = new AService();
            container.RegisterInstance<AService, IService>(registeredInstance);
        };

        Because of = () =>
            instance = container.Get<IService>();

        It should_return_the_same_instance = () =>
            instance.ShouldBeTheSameAs(registeredInstance);
    }
}