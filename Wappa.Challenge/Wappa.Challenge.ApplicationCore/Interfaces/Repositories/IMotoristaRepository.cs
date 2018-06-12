using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Challenge.ApplicationCore.Entities;

namespace Wappa.Challenge.ApplicationCore.Interfaces.Repositories
{
    public interface IMotoristaRepository : IBaseRepository<Motorista>
    {
        Motorista Adicionar(Motorista entity);
        bool Apagar(long id);
    }
}
