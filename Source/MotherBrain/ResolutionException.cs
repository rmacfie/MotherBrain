namespace MotherBrain
{
    using System;

    [Serializable]
    public class ResolutionException : MotherBrainException
    {
        public ResolutionException(string message) : base(message)
        {
        }
    }
}