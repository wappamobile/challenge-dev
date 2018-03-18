using Microsoft.Extensions.Configuration;
using System;
using Wappa.DataAccess;
using Wappa.Models;
using Xunit;
using System.Linq;

namespace Wappa.Tests
{
    public class DapperRepositoryTests
    {

        [Fact]
        public void Insert()
        {
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();
            var taxista = new Taxista
            {
                Marca = "VW",
                Modelo = "Santana",
                PrimeiroNome = "Augustinho",
                UltimoNome = "Carrara",
                Placa = "XYZ-9876"
            };
            var repository = new DapperTaxistaRepository(config);
            var id = repository.Insert(taxista);
            Assert.True(id > 0);
        }
        [Fact]
        public void Delete()
        {
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();

            var repository = new DapperTaxistaRepository(config);
            var rows = repository.Delete(1);
            Assert.True(rows > 0);
        }
        [Fact]
        public void Update()
        {
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();
            var taxista = new Taxista
            {
                Marca = "VW",
                Modelo = "Santana",
                PrimeiroNome = "Augustinho",
                UltimoNome = "Carrara",
                Placa = "XYZ-9876",
                IdTaxista = 2
            };
            var repository = new DapperTaxistaRepository(config);
            var rows = repository.Update(taxista);
            Assert.True(rows > 0);
        }
        [Fact]
        public void List()
        {
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();

            var repository = new DapperTaxistaRepository(config);
            var lista = repository.List();
            Assert.True(lista.Any());
        }
        [Fact]
        public void Find()
        {
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();

            var repository = new DapperTaxistaRepository(config);
            var taxista = repository.Find(2);
            Assert.True(taxista.IdTaxista == 2);
        }
        [Fact]
        public void PagedList()
        {
            int pageSize = 1;
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();

            var repository = new DapperTaxistaRepository(config);
            var lista = repository.PagedList(pageSize, 1, 0, 0);
            Assert.True(lista.Count <= pageSize);
        }
    }
}
