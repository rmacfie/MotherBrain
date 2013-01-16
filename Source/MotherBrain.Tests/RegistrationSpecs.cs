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

    public class When_registering_constant_with_null : With_container
    {
        static Exception caughtException;

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterConstant<IService>(null));

        It should_throw = () =>
            caughtException.ShouldBeOfType<ArgumentNullException>();
    }

    public class When_registering_singleton_the_same_type_twice : With_container
    {
        static Exception caughtException;

        Establish context = () =>
            container.RegisterSingletonPerContainer<IService>(c => new AService());

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterSingletonPerContainer<IService>(c => new AService()));

        It should_throw = () =>
            caughtException.ShouldBeOfType<RegistrationException>();
    }

    public class When_registering_singleton_with_null : With_container
    {
        static Exception caughtException;

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterSingletonPerContainer<IService>(null));

        It should_throw = () =>
            caughtException.ShouldBeOfType<ArgumentNullException>();
    }

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

    public class When_registering_with_null_factory : With_container
    {
        static Exception caughtException;

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterTransient<IService>(null));

        It should_throw = () =>
            caughtException.ShouldBeOfType<ArgumentNullException>();
    }
}