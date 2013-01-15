namespace MotherBrain.Tests
{
    using Machine.Specifications;

    public class When_comparing_keys_with_different_types_and_same_names
    {
        static Key key1;
        static Key key2;

        Because of = () =>
        {
            key1 = new Key(typeof(int));
            key2 = new Key(typeof(long));
        };

        It should_not_be_equal = () =>
            key1.Equals(key2).ShouldBeFalse();
    }
}