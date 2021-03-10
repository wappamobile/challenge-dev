using System;
using Wappa.Core.DomainObjects;

namespace Wappa.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}