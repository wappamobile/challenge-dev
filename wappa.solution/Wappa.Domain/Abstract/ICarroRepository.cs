using System;
using System.Collections.Generic;
using System.Linq;
using Wappa.Domain.Entities;

namespace Wappa.Domain.Abstract
{
    public interface ICarroRepository
    {
        List<Carro> get();
        List<Carro> get(Marca marca);
        List<Carro> get(Modelo modelo);
        Carro get(int id);
        Carro get(string placa);
        void add(Carro carro);
        void update(Carro carro);
        void delete(int id);
    }
}