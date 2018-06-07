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
    public class CarrosRepositorio<T> : RepositorioBase<Carros>, ICarrosRepositorio where T : DbContext, new()
    {
        public CarrosRepositorio()
            : this(new T()) { }

        public CarrosRepositorio(T ctx)
            : base(ctx)
        {

        }

    }

}
