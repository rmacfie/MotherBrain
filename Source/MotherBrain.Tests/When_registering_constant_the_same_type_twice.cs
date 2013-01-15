namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_registering_constant_the_same_type_twice : With_container
    {
        static Exception caughtException;

        Establish context = () =>
            container.RegisterConstant<IService>(new AService());

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterConstant<IService>(new AService()));

        It should_throw = () =>
            caughtException.ShouldBeOfType<RegistrationException>();
    }
}