using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFC.Wappa.Teste.Base.Core.Repositorios;
using WebFC.Wappa.Teste.Base.Core.Models;

namespace WebFc.Wappa.Teste.Base.EF.Repositorio
{
    public class MotoristasRepositorio<T> : RepositorioBase<Motoristas>, IMotoristasRepositorio where T : DbContext, new()
    {
        public MotoristasRepositorio()
            : this(new T()) { }

        public MotoristasRepositorio(T ctx)
            : base(ctx)
        {

        }

    }
}
