using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Wappa.Contracts;
using Wappa.Contracts.Models;
using Wappa.GeoLocalization;

namespace Wappa.Tests
{   
    public class GeoCodingTests
    {
        GeoLocator locator = new GeoLocator();        

        [Test]
        public async Task GeoCoding_Shall_Work_With_Valid_Address()
        {
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                        .Build();

            var address = $"Rua das Palmeiras, 127, Gopouva, Guarulhos, São Paulo, Brasil";

            var location = await locator.GetLocation(config, address);

            Assert.IsNotNull(location);
        }

    }
}
