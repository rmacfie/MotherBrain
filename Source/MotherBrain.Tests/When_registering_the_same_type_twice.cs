namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_registering_the_same_type_twice
    {
        static IContainer container;
        static Exception caughtException;

        Establish context = () =>
        {
            container = new Container();
            container.RegisterTransient<AService, IService>(c => new AService());
        };

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterTransient<AService, IService>(c => new AService()));

        It should_throw = () =>
            caughtException.ShouldBeOfType<RegistrationException>();
    }
}