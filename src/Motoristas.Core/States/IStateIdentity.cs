namespace Motoristas.Core.States
{
    public interface IStateIdentity<out TId>
    {
        TId Id { get; }
    }
}
