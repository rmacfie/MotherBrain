namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_registering_constant_with_null : With_container
    {
        static Exception caughtException;

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterConstant<IService>(null));

        It should_throw = () =>
            caughtException.ShouldBeOfType<ArgumentNullException>();
    }
}