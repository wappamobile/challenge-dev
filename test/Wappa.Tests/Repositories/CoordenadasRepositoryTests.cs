using System;
using System.Collections.Generic;
using Xunit;
using Wappa.Core.Interfaces;
using Wappa.Core.Models;
using Wappa.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Wappa.Tests
{
    public class CoordenadasRepositoryTests
    {
        private readonly IConfigurationRoot _config;
        
        private readonly ICoordenadasRepository _rep;

        public CoordenadasRepositoryTests()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                .Build();

            var services = new ServiceCollection()
                .AddTransient<ICoordenadasRepository, GoogleMapsCoordenadasRepository>(
                    x => new GoogleMapsCoordenadasRepository(
                        _config["GoogleMapsApi:BaseUrl"], 
                        _config["GoogleMapsApi:Key"]));

            var serviceProvider = services.BuildServiceProvider();
            _rep = serviceProvider.GetService<ICoordenadasRepository>();            
        }

        [Fact]
        public void Repositorio_Deve_Retornar_Coordenadas()
        {
            var endereco = new Endereco { Logradouro = "Avenida Conceição", Numero = "367", Cidade = "São Paulo", Pais = "Brasil" };

            var retorno = _rep.Get(endereco).Result;

            Assert.True(retorno != null);
            Assert.True(retorno.Resultados != null);
            Assert.True(1 == retorno.Resultados.Count());
            Assert.Equal(-23.4998164, retorno.Resultados.First().Geometria.Localizacao.Latitude);
            Assert.Equal(-23.4998164, retorno.Resultados.First().Geometria.Localizacao.Latitude);
        }        
    }
}
