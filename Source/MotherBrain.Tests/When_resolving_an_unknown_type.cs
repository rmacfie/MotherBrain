namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_resolving_an_unknown_type
    {
        static IContainer container;
        static Exception caughtException;

        Establish context = () =>
        {
            container = new Container();
        };

        Because of = () =>
            caughtException = Catch.Exception(() => container.Get<IService>());

        It should_throw = () =>
            caughtException.ShouldBeOfType<ResolvanceException>();
    }
}