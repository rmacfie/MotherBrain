namespace MotherBrain
{
    using System;

    [Serializable]
    public class RegistrationException : MotherBrainException
    {
        public RegistrationException(string message) : base(message)
        {
        }
    }
}