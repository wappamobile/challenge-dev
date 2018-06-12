using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WappaMobile.Driver.API.Model;

namespace WappaMobile.Driver.API.Infrastructure
{
    public class DriverContext
    {
        private readonly IMongoDatabase _database = null;

        public DriverContext(IOptions<DriverSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
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
