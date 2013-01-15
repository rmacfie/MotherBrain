namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_registering_instance_with_null
    {
        static IContainer container;
        static Exception caughtException;

        Establish context = () =>
            container = new Container();

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterInstance<AService, IService>(null));

        It should_throw = () =>
            caughtException.ShouldBeOfType<ArgumentNullException>();
    }
}