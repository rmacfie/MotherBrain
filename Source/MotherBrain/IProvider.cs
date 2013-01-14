namespace MotherBrain
{
    public interface IProvider
    {
        object GetInstance(IContainer container);
    }
}