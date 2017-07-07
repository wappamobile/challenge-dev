using System;
using System.Collections.Generic;
using System.Linq;
using Wappa.Domain.Entities;

namespace Wappa.Domain.Abstract
{
    public interface IModeloRepository
    {
        List<Modelo> get();
        List<Modelo> get(String descricao);
        List<Modelo> get(Marca marca);
        Modelo get(int id);
        void add(Modelo modelo);
        void update(Modelo modelo);
        void delete(int id);
    }
}