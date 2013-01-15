namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public class When_comparing_keys_with_same_types_and_null_names
    {
        private static Key key1;
        private static Key key2;

        Because of = () =>
        {
            key1 = new Key(typeof(int), null);
            key2 = new Key(typeof(int), null);
        };

        It should_be_equal = () =>
            key1.Equals(key2).ShouldBeTrue();
    }
}