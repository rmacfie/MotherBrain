namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

	public class When_resolving_an_unknown_type : With_container
    {
        static Exception caughtException;

        Because of = () =>
            caughtException = Catch.Exception(() => container.Get<IService>());

        It should_throw = () =>
            caughtException.ShouldBeOfType<ResolvanceException>();
    }
}