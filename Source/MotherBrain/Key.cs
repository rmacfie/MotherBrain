namespace MotherBrain
{
    using System;

    public class Key
    {
        public readonly string Name;
        public readonly Type Type;

        public Key(Type type)
        {
            Type = type;
            Name = null;
        }

        public Key(Type type, string name)
        {
            Type = type;
            Name = name;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Type.GetHashCode() * 397) ^ (Name == null ? 0 : Name.GetHashCode());
            }
        }

        protected bool Equals(Key other)
        {
            return Equals(Type, other.Type) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (other.GetType() != GetType())
                return false;

            return Equals((Key)other);
        }

        public static bool operator ==(Key left, Key right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Key left, Key right)
        {
            return !Equals(left, right);
        }
    }
}