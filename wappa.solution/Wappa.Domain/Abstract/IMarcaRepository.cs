using System;
using System.Collections.Generic;
using System.Linq;
using Wappa.Domain.Entities;

namespace Wappa.Domain.Abstract
{
    public interface IMarcaRepository
    {
        List<Marca> get();
        List<Marca> get(String descricao);
        Marca get(int id);
        void add(Marca marca);
        void update(Marca marca);
        void delete(int id);
    }
}