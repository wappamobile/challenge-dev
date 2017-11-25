using Microsoft.EntityFrameworkCore;

namespace TesteDev.Infra.Repositorios.Interfaces
{
    public interface IRepositorioBase<T> where T : class
    {
        T Buscar(int id);
        T Criar(T entidade);
        T Atualizar(T entidade);
        void Remover(int id);
        bool VerificarExistencia(int id);
        DbSet<T> Entidades { get; }
    }
}
