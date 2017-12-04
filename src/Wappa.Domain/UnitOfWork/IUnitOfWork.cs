using Wappa.Domain.Repositories;

namespace Wappa.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IMotoristaRepository GetMotoristaRepository();
        ICarroRepository GetCarroRepository();
        ICidadeRepository GetCidadeRepository();

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
