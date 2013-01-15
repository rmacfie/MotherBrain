namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_registering_with_null_factory : With_container
    {
        static Exception caughtException;

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterTransient<IService>(null));

        It should_throw = () =>
            caughtException.ShouldBeOfType<ArgumentNullException>();
    }
}