using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WappaMobile.Driver.API.Model;

namespace WappaMobile.Driver.Infrastructure
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _database = null;

        public DbContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<DriverRegistry> Driver
        {
            get
            {
                return _database.GetCollection<DriverRegistry>("Driver");
            }
        }
    }
}
