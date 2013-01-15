using System;

namespace MotherBrain
{
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
				return ((Type != null ? Type.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			}
		}

		public static bool operator ==(Key left, Key right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Key left, Key right)
		{
			return !Equals(left, right);
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
	}
}