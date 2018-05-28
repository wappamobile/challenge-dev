using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wappa.Core.UnitOfWork
{
    /// <summary>
    /// A 'UnitOfWork' usará o objeto DBContext mas, ao contrário dos repositórios,
    /// ele se concentrará apenas no gerenciamento de transações, ou seja, chamando o método SaveChanges ().
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
