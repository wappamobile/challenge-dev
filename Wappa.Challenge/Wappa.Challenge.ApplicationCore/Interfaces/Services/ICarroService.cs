using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Challenge.ApplicationCore.Entities;

namespace Wappa.Challenge.ApplicationCore.Interfaces.Services
{
    public interface ICarroService : IBaseService<Carro>
    {
        bool Apagar(long id);
    }
}
