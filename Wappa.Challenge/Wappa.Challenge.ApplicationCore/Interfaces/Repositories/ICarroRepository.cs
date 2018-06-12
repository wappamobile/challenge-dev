using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Challenge.ApplicationCore.Entities;

namespace Wappa.Challenge.ApplicationCore.Interfaces.Repositories
{
    public interface ICarroRepository : IBaseRepository<Carro>
    {
        bool Apagar(long id);
    }
}
