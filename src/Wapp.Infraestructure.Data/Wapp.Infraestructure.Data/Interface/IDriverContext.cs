using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Infrastructure.Data.Models;

namespace Wappa.Infrastructure.Data.Interface
{
    public interface IDriverContext
    {
        IMongoCollection<Driver> Drivers { get; }
    }
}
