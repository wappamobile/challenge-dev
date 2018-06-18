using ApplicationCore.Entity;
using ApplicationCore.Interfaces.Repository;
using Infra.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository
{
    public class CarroRepository : Repository<Carro>, ICarroRepository
    {
        public CarroRepository(Context dbContext) : base(dbContext)
        {

        }
    }
}
