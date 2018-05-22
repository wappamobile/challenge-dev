using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Wappa.Core.Models;
using Wappa.Service.Services;
using Wappa.Core.Interfaces;
using System.Linq;

namespace Wappa.Tests
{
    public class MotoristaServiceTests
    {
        private Mock<IMotoristaRepository> _repMotoristaMock { get; }

        private Mock<ICoordenadasRepository> _repCoordenadasMock { get; }

        private readonly IMotoristaService _service;

        public MotoristaServiceTests()
        {            
            _repMotoristaMock = new Mock<IMotoristaRepository>();
            _repCoordenadasMock = new Mock<ICoordenadasRepository>();

            SetupMocks();
            
            // var motoristas = new List<Motorista> 
            // { 
            //     new Motorista() { Id = 1, PrimeiroNome = "Arya", UltimoNome = "Stark" }
            // };

            // _repMotoristaMock.Setup(x => x.GetAll(null)).ReturnsAsync(motoristas.AsQueryable());

            // _repMotoristaMock.Setup(x => x.GetAll("PrimeiroNome")).ReturnsAsync(motoristas.AsQueryable());

            // _repMotoristaMock.Setup(x => x.GetAll("UltimoNome")).ReturnsAsync(motoristas.AsQueryable());
            
            // _repMotoristaMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((int id) => motoristas.Find(s => s.Id == id));

            // _repMotoristaMock.Setup(x => x.Save(It.IsAny<Motorista>()))
            //     .Callback((Motorista motorista) => motoristas.Add(motorista));

            // _repMotoristaMock.Setup(x => x.Update(It.IsAny<Motorista>()))
            //     .Callback((Motorista motorista) => motoristas[motoristas.FindIndex(x => x.Id == motorista.Id)] = motorista);

            // _repMotoristaMock.Setup(x => x.Delete(It.IsAny<int>()))
            //     .Callback((int id) => motoristas.RemoveAt(motoristas.FindIndex(x => x.Id == id)));

            var services = new ServiceCollection()
                .AddTransient<IMotoristaService, MotoristaService>(
                    x => new MotoristaService(_repMotoristaMock.Object, _repCoordenadasMock.Object));

            var serviceProvider = services.BuildServiceProvider();
            _service = serviceProvider.GetService<IMotoristaService>();            
        }

        private void SetupMocks()
        {
            var motoristas = new List<Motorista> 
            { 
                new Motorista() { Id = 1, PrimeiroNome = "Arya", UltimoNome = "Stark" }
            };

            SetupGetAll(motoristas);
            SetupGetAllOrderByPrimeiroNome(motoristas);
            SetupGetAllOrderByUltimoNome(motoristas);
            SetupGetById(motoristas);
            SetupSave(motoristas);
            SetupUpdate(motoristas);
            SetupDelete(motoristas);
        }

        private void SetupGetAll(List<Motorista> motoristas)
        {
            _repMotoristaMock.Setup(x => x.GetAll(null)).ReturnsAsync(motoristas.AsQueryable());
        }

        private void SetupGetAllOrderByPrimeiroNome(List<Motorista> motoristas)
        {
            _repMotoristaMock.Setup(x => x.GetAll("PrimeiroNome")).ReturnsAsync(motoristas.AsQueryable());
        }

        private void SetupGetAllOrderByUltimoNome(List<Motorista> motoristas)
        {
            _repMotoristaMock.Setup(x => x.GetAll("UltimoNome")).ReturnsAsync(motoristas.AsQueryable());
        }

        private void SetupGetById(List<Motorista> motoristas)
        {
            _repMotoristaMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((int id) => motoristas.Find(s => s.Id == id));
        }

        private void SetupSave(List<Motorista> motoristas)
        {
            _repMotoristaMock.Setup(x => x.Save(It.IsAny<Motorista>()))
                .Callback((Motorista motorista) => motoristas.Add(motorista));
        }

        private void SetupUpdate(List<Motorista> motoristas)
        {
            _repMotoristaMock.Setup(x => x.Update(It.IsAny<Motorista>()))
                .Callback((Motorista motorista) => motoristas[motoristas.FindIndex(x => x.Id == motorista.Id)] = motorista);
        }

        private void SetupDelete(List<Motorista> motoristas)
        {
            _repMotoristaMock.Setup(x => x.Delete(It.IsAny<int>()))
                .Callback((int id) => motoristas.RemoveAt(motoristas.FindIndex(x => x.Id == id)));
        }

        [Fact]
        public void Service_Deve_Salvar_Novo_Motorista()
        {
            var motorista = new Motorista
            {
                PrimeiroNome = "Ned",
                UltimoNome = "Stark"
            };

            _service.Save(motorista);
            _repMotoristaMock.Verify(x => x.Save(It.IsAny<Motorista>()), Times.Once);
            var motoristas = _service.GetAll().Result;

            Assert.True(2 == motoristas.Count());
        }

        [Fact]
        public void Service_Deve_Atualizar_Motorista()
        {
            var motorista = new Motorista
            {
                Id = 1,
                PrimeiroNome = "Aegon",
                UltimoNome = "Targaryen",
            };

            _service.Update(motorista);
            _repMotoristaMock.Verify(x => x.Update(It.IsAny<Motorista>()), Times.Once);
            var motoristas = _service.GetAll().Result;

            Assert.True(1 == motoristas.Count());
            Assert.Equal("Aegon", motoristas.First().PrimeiroNome);
            Assert.Equal("Targaryen", motoristas.First().UltimoNome);
        }

        [Fact]
        public void Service_Deve_Excluir_Motorista()
        {
            const int id = 1;
            
            var motorista = _service.Get(id);
            _service.Delete(motorista.Id);            
            _repMotoristaMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
            _repMotoristaMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);

            Assert.True(0 == _service.GetAll().Result.Count());
        }

        [Fact]
        public void Service_Deve_Obter_Todos_Motoristas()
        {
            var motoristas = _service.GetAll().Result;
            
            _repMotoristaMock.Verify(x => x.GetAll(null), Times.Once);

            Assert.True(1 == motoristas.Count());
        }

        [Theory]
        [InlineData(1)]
        public void Service_Deve_Obter_Motorista_PorId(int id)
        {            
            var retorno = _service.Get(id).Result;

            Assert.True(retorno != null);
            Assert.Equal("Arya", retorno.PrimeiroNome);
            Assert.Equal("Stark", retorno.UltimoNome);
        }

        [Fact]
        public void Service_Deve_Obter_Motoristas_Ordenados_Por_Primeiro_Nome()
        {
            var motorista = new Motorista
            {
                PrimeiroNome = "Ned"
            };

            _service.Save(motorista);            
            var motoristas = _service.GetAll("PrimeiroNome").Result;

            Assert.True(2 == motoristas.Count());
            Assert.Equal("Arya", motoristas.First().PrimeiroNome);
            Assert.Equal("Ned", motoristas.Last().PrimeiroNome);
        }

        [Fact]
        public void Service_Deve_Obter_Motoristas_Ordenados_Por_Ultimo_Nome()
        {
            var motorista = new Motorista
            {
                UltimoNome = "Targaryen"
            };

            _service.Save(motorista);            
            var motoristas = _service.GetAll("UltimoNome").Result;

            Assert.True(2 == motoristas.Count());
            Assert.Equal("Stark", motoristas.First().UltimoNome);
            Assert.Equal("Targaryen", motoristas.Last().UltimoNome);
        }
    }
}
