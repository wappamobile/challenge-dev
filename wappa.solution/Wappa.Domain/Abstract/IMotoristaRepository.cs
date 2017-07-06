using System;
using System.Collections.Generic;
using System.Linq;
using Wappa.Domain.Entities;

namespace Wappa.Domain.Abstract
{
    public interface IMotoristaRepository
    {
        List<Motorista> get();
        Motorista get(int id);
        void add(Motorista motorista);
        void update(Motorista motorista);
        void delete(int id);
    }
}