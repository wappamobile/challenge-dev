using System;
using System.Collections.Generic;
using System.Linq;
using Wappa.Domain.Entities;

namespace Wappa.Domain.Abstract
{
    public interface IEnderecoRepository
    {
        List<Endereco> get();
        Endereco get(int id);
        void add(Endereco endereco);
        void update(Endereco endereco);
        void delete(int id);
    }
}
