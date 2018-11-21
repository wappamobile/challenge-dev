using Domain.Model;
using System.Collections.Generic;
using Domain.Repository;
using Domain.Enumerator;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Domain.Service
{
    public interface IDriverService
    {
        string Save(ILoggerFactory loggerFactory, ChallengeDevEntityContext context, IHttpClientFactory clientFactory, IGeocodingRepository repository, IGeocodingService geocodingService, string geocodingApiKey, Driver driver);
        ICollection<Driver> List(ILoggerFactory loggerFactory, ChallengeDevEntityContext context, DriverNameOrdenation order);
        string Update(ILoggerFactory loggerFactory, ChallengeDevEntityContext context, IHttpClientFactory clientFactory, IGeocodingRepository repository, IGeocodingService geocodingService, string geocodingApiKey, Driver driver);
        string Delete(ILoggerFactory loggerFactory, ChallengeDevEntityContext context, int id);
    }
}
