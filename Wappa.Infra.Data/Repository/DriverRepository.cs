using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Domain.Interfaces;
using Wappa.Domain.Models;
using Wappa.Infra.Data.Context;

namespace Wappa.Infra.Data.Repository
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(WappaContext context)
            : base(context)
        {

        }        
    }    
}
