namespace Motoristas.Core
{
    public interface IAggregateRoot
    {
        object GetState();
    }
}
