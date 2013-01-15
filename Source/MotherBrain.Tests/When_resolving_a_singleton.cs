namespace MotherBrain.Tests
{
	using Machine.Specifications;

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
}