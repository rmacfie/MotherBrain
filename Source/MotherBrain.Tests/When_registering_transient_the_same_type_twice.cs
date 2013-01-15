namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_registering_transient_the_same_type_twice : With_container
    {
        static Exception caughtException;

        Establish context = () =>
            container.RegisterTransient<IService>(c => new AService());

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterTransient<IService>(c => new AService()));

        It should_throw = () =>
            caughtException.ShouldBeOfType<RegistrationException>();
    }
}