namespace MotherBrain
{
    using System;

    [Serializable]
    public class ResolvanceException : MotherBrainException
    {
        public ResolvanceException(string message) : base(message)
        {
        }
    }
}