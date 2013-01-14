namespace MotherBrain
{
    using System;

    [Serializable]
    public class MotherBrainException : Exception
    {
        public MotherBrainException(string message) : base(message)
        {
        }
    }
}