namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public class When_comparing_keys_with_same_types_and_different_names
    {
        static Key key1;
        static Key key2;

        Because of = () =>
        {
            key1 = new Key(typeof(int), "a");
            key2 = new Key(typeof(int), "b");
        };

        It should_not_be_equal = () =>
            key1.Equals(key2).ShouldBeFalse();
    }
}