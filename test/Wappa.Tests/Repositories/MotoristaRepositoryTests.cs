using System;
using System.Collections.Generic;
using Xunit;
using Wappa.Core.Interfaces;
using Wappa.Core.Models;
using Wappa.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Wappa.Tests
{
    public class MotoristaRepositoryTests
    {
        private readonly IConfigurationRoot _config;

        private readonly IMotoristaRepository _rep;

        public MotoristaRepositoryTests()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                .Build();

            var services = new ServiceCollection()
                .AddTransient<IMotoristaRepository, MotoristaRepository>(
                    x => new MotoristaRepository(
                        Environment.CurrentDirectory + "\\" + _config["LiteDatabase:ConnectionString"]));

            var serviceProvider = services.BuildServiceProvider();
            _rep = serviceProvider.GetService<IMotoristaRepository>();            
        }

        [Fact]
        public void Repositorio_Deve_Salvar_Novo_Motorista()
        {
            var motorista = new Motorista { PrimeiroNome = "Ned", UltimoNome = "Stark" };

            var retorno = _rep.Save(motorista).Result;            

            Assert.True(retorno != null);
            Assert.Equal(retorno.PrimeiroNome, motorista.PrimeiroNome);
            Assert.Equal(retorno.UltimoNome, motorista.UltimoNome);
        }

        [Fact]
        public void Repositorio_Deve_Atualizar_Motorista()
        {
            var motorista = new Motorista { PrimeiroNome = "John", UltimoNome = "Snow" };
            _rep.Save(motorista);            
            motorista.PrimeiroNome = "Aegon";
            motorista.UltimoNome = "Targaryen";

            var retorno = _rep.Update(motorista).Result;

            Assert.True(retorno != null);
            Assert.True(retorno.PrimeiroNome == motorista.PrimeiroNome);
            Assert.True(retorno.UltimoNome == motorista.UltimoNome);
        }

        [Fact]
        public void Repositorio_Deve_Excluir_Motorista()
        {
            var motorista = new Motorista{ PrimeiroNome = "Cersei" };
            _rep.Save(motorista);

            var retorno = _rep.Delete(motorista.Id).Result;

            Assert.Equal(retorno, motorista.Id);
        }

        [Fact]
        public void Repositorio_Deve_Obter_Motorista()
        {
            var motorista = new Motorista{ PrimeiroNome = "Arya", UltimoNome = "Stark" };;
            _rep.Save(motorista);

            var retorno = _rep.Get(motorista.Id).Result;

            Assert.True(retorno != null);
            Assert.Equal(retorno.PrimeiroNome, motorista.PrimeiroNome);
            Assert.Equal(retorno.UltimoNome, motorista.UltimoNome);
        }
    }
}
