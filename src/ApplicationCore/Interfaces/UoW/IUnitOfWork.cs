using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
