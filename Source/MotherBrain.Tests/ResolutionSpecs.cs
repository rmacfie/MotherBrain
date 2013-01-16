namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_resolving_a_composed_object : With_container
    {
        static IComposedService instance;

        Establish context = () =>
        {
            container.RegisterTransient<IService>(c => new AService());
            container.RegisterTransient<IComposedService>(c => new AComposedService(c.Get<IService>()));
        };

        Because of = () =>
            instance = container.Get<IComposedService>();

        It should_return_instance = () =>
            instance.ShouldNotBeNull();
    }

    public class When_resolving_a_concrete_registered_as_itself : With_container
    {
        static IService instance;

        Establish context = () =>
            container.RegisterTransient(c => new AService());

        Because of = () =>
            instance = container.Get<AService>();

        It should_return_instance = () =>
            instance.ShouldNotBeNull();
    }

    public class When_resolving_a_constant : With_container
    {
        static IService instance;
        static AService registeredInstance;

        Establish context = () =>
        {
            registeredInstance = new AService();
            container.RegisterConstant<IService>(registeredInstance);
        };

        Because of = () =>
            instance = container.Get<IService>();

        It should_return_the_same_instance = () =>
            instance.ShouldBeTheSameAs(registeredInstance);
    }

    public class When_resolving_a_simple_object : With_container
    {
        static IService instance;

        Establish context = () =>
            container.RegisterTransient<IService>(c => new AService());

        Because of = () =>
            instance = container.Get<IService>();

        It should_return_instance = () =>
            instance.ShouldNotBeNull();
    }

    public class When_resolving_a_singleton : With_container
    {
        static IService instance1;
        static IService instance2;

        Establish context = () =>
            container.RegisterSingleton<IService>(c => new AService());

        Because of = () =>
        {
            instance1 = container.Get<IService>();
            instance2 = container.Get<IService>();
        };

        It should_return_the_same_instance_every_time = () =>
            instance1.ShouldBeTheSameAs(instance2);
    }

    public class When_resolving_an_unknown_type : With_container
    {
        static Exception caughtException;

        Because of = () =>
            caughtException = Catch.Exception(() => container.Get<IService>());

        It should_throw = () =>
            caughtException.ShouldBeOfType<ResolutionException>();
    }
}