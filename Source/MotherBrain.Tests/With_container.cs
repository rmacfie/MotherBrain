namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public abstract class With_container
    {
        protected static IContainer container;

        Cleanup clean = () =>
            container.Dispose();

        Establish context = () =>
            container = new Container();
    }
}