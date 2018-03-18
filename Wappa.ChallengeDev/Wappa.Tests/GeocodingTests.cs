using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Moq;
using Wappa.DataAccess;
using Wappa.Infrastructure;
using Wappa.Models;
using Xunit;

namespace Wappa.Tests
{
    public class GeocodingTests
    {

        [Fact]
        public async Task Can_Search_Coordinates()
        {

            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();
            var repository = new GeocodingProxy(config);
            var localizacao = await repository.BuscarCoordenadasGeograficas("Avenida Bariloche, 13");
            Assert.NotNull(localizacao);
        }
    }
}
