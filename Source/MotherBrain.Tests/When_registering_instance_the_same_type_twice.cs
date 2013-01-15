namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_registering_instance_the_same_type_twice
    {
        static IContainer container;
        static Exception caughtException;

        Establish context = () =>
        {
            container = new Container();
            container.RegisterInstance<AService, IService>(new AService());
        };

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterInstance<AService, IService>(new AService()));

        It should_throw = () =>
            caughtException.ShouldBeOfType<RegistrationException>();
    }
}