using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WappaMobile.Driver.API.Model;

namespace WappaMobile.Driver.Infrastructure
{
    public interface IDbContext
    {
        IMongoCollection<DriverRegistry> Driver { get; }
    }
}
