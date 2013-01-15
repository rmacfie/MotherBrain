﻿namespace MotherBrain.Tests
{
    using System;
    using Machine.Specifications;

	public class When_registering_instance_the_same_type_twice : With_container
    {
        static Exception caughtException;

        Establish context = () =>
			container.RegisterInstance<IService>(new AService());

        Because of = () =>
            caughtException = Catch.Exception(() => container.RegisterInstance<IService>(new AService()));

        It should_throw = () =>
            caughtException.ShouldBeOfType<RegistrationException>();
    }
}