namespace Motoristas.Core
{
    public interface IIdentityGenerator<out T>
    {
        T Create();
    }
}
