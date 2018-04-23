using System;
using System.Collections.Generic;
using System.Linq;
using Wappa.Challenge.Dev.Data;
using Wappa.Challenge.Dev.Models;

namespace Wappa.Challenge.Dev.Data.Impl
{
    public class MotoristaRepositorio : BaseRepositorio<Motorista>, IBaseRepositorio<Motorista>
    {
        public IEnumerable<Motorista> Queryable => base.Listar();

        public int Salvar(Motorista entidade)
        {
            return base.Gravar(entidade);
        }

        public new int Excluir(int id)
        {
            return base.Excluir(id);
        }

        public Motorista Obter(int ID)
        {
            return Queryable.FirstOrDefault(m => m.ID == ID);
        }
    }
}
