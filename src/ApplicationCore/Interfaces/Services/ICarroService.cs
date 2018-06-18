using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces.Services
{
    public interface ICarroService
    {
        void Add(Carro carro);
        void Update(Carro carro);
    }
}
