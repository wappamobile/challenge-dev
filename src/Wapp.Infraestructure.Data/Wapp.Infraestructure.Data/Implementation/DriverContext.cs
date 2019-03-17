using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Infrastructure.Data.Interface;
using Wappa.Infrastructure.Data.Models;

namespace Wappa.Infrastructure.Data.Implementation
{
    public class DriverContext : IDriverContext
    {
        private readonly IMongoDatabase _db;
        //private readonly IConfiguration _configuration;

        public DriverContext(IConfiguration config)
        {
            var client = new MongoClient(config["Database:ConnectionString"]);
            _db = client.GetDatabase(config["Database:Name"]);
        }

        public IMongoCollection<Driver> Drivers => _db.GetCollection<Driver>("Drivers");

    }
}
