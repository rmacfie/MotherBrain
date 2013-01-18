namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

    public class When_dynamically_creating_simple_object
    {
        static DynamicCreator<AService> creator;
        static AService instance;

        Establish context = () => { creator = new DynamicCreator<AService>(); };

        Because of = () => { instance = creator.Create(t => null); };

        It should_create_an_instance = () =>
        {
            instance.ShouldNotBeNull();
            instance.ShouldBeOfType<AService>();
        };
    }

    public class When_dynamically_creating_complex_object
    {
        static DynamicCreator<AComposedService> creator;
        static Func<Type, object> factory;
        static AComposedService instance;

        Establish context = () =>
        {
            factory = t => t == typeof(IService) ? new AService() : null;
            creator = new DynamicCreator<AComposedService>();
        };

        Because of = () => { instance = creator.Create(factory); };

        It should_create_an_instance = () =>
        {
            instance.ShouldNotBeNull();
            instance.ShouldBeOfType<AComposedService>();
        };
    }
}