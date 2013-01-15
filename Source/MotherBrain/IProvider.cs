namespace MotherBrain
{
    public interface IProvider
    {
        /// <summary>
        /// The Key that was used to register this provider
        /// </summary>
        Key Key { get; }

        /// <summary>
        /// Gets the instance
        /// </summary>
        object GetInstance(IContainer container);
    }
}