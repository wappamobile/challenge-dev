using ApplicationCore.Entity;
using ApplicationCore.Interfaces.Repository;
using Infra.Data;
using Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Linq;

namespace Infra.Repository
{
    public class MotoristaRepository : Repository<Motorista>, IMotoristaRepository
    {
        public MotoristaRepository(Context dbContext) : base(dbContext)
        {

        }

        public IPagedList<Motorista> Listar(int pageNumber, int pageSize, string sortBy)
        {
            var query = GetQuery()
                .Include(x => x.Carro)
                .Include(x => x.Endereco)
                .Include(x => x.Endereco.GeoLocation)
                .Where(x => x.Ativo);

            if (!string.IsNullOrEmpty(sortBy))
                query = query.Sort(sortBy);

            return query.ToPagedList(pageNumber, pageSize);
        }
    }
}
