namespace MotherBrain.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Machine.Specifications;

    public class When_resolving_all_registrations_with_the_same_type : With_container
    {
        static List<IService> instances;

        Establish context = () =>
        {
            container.RegisterTransient<IService>(c => new AService());
            container.RegisterTransient<IService>(c => new AService2(), "2");
            container.RegisterSingletonPerContainer<IService>(c => new AService3(), "3");
            container.RegisterConstant<IService>(new AService4(), "4");
        };

        Because of = () => { instances = container.GetAll<IService>().ToList(); };

        It should_resolve_correctly = () =>
        {
            instances.Count.ShouldEqual(4);
            instances.OfType<AService>().Count().ShouldEqual(1);
            instances.OfType<AService2>().Count().ShouldEqual(1);
            instances.OfType<AService3>().Count().ShouldEqual(1);
            instances.OfType<AService4>().Count().ShouldEqual(1);
        };
    }

    public class When_resolving_several_registrations_with_the_same_type_but_different_names : With_container
    {
        static List<IService> instances;
        static List<IService> instances2;

        Establish context = () =>
        {
            instances = new List<IService>();
            instances2 = new List<IService>();

            container.RegisterTransient<IService>(c => new AService());
            container.RegisterTransient<IService>(c => new AService2(), "2");
            container.RegisterSingletonPerContainer<IService>(c => new AService2(), "3");
            container.RegisterConstant<IService>(new AService(), "4");
        };

        Because of = () =>
        {
            instances.Add(container.Get<IService>());
            instances2.Add(container.Get<IService>());
            instances.Add(container.Get<IService>("2"));
            instances2.Add(container.Get<IService>("2"));
            instances.Add(container.Get<IService>("3"));
            instances2.Add(container.Get<IService>("3"));
            instances.Add(container.Get<IService>("4"));
            instances2.Add(container.Get<IService>("4"));
        };

        It should_resolve_correctly = () =>
        {
            // TODO: Split this up?
            instances[0].ShouldBeOfType<AService>();
            instances[0].ShouldNotBeTheSameAs(instances2[0]);
            instances[1].ShouldBeOfType<AService2>();
            instances[1].ShouldNotBeTheSameAs(instances2[1]);
            instances[2].ShouldBeOfType<AService2>();
            instances[2].ShouldBeTheSameAs(instances2[2]);
            instances[3].ShouldBeOfType<AService>();
            instances[3].ShouldBeTheSameAs(instances2[3]);
        };
    }

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
            container.RegisterSingletonPerContainer<IService>(c => new AService());

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