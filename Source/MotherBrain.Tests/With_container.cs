namespace MotherBrain.Tests
{
	using Machine.Specifications;

	public abstract class With_container
	{
		protected static IContainer container;

		Establish context = () =>
			container = new Container();

		Cleanup clean = () =>
			container.Dispose();
	}
}