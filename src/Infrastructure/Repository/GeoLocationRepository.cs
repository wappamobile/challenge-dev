using ApplicationCore.Entity;
using ApplicationCore.Interfaces.Repository;
using Infra.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository
{
    public class GeoLocationRepository : Repository<GeoLocation>, IGeoLocationRepository
    {
        public GeoLocationRepository(Context dbContext) : base(dbContext)
        {

        }
    }
}
