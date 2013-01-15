namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_registering_with_null_factory
    {
        static IContainer container;
        static Exception caughtException;

        Establish context = () =>
            container = new Container();

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterTransient<AService, IService>(null));

        It should_throw = () =>
            caughtException.ShouldBeOfType<ArgumentNullException>();
    }
}