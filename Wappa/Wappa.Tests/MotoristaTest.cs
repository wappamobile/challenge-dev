using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Entities;
using Wappa.Services;
using Wappa.Services.Interfaces;
using Wappa.Core.Repositories;

namespace Wappa.Test
{
    [TestClass]
    public class MotoristaTest
    {
        private readonly Mock<IRepositoryBase<Motorista>> _moqMotoristaRepository;
        private readonly Mock<ICarroService> _moqCarroService;
        private readonly Mock<IEnderecoService> _moqEnderecoService;
        private readonly MotoristaService _motoristaService;
        private IEnumerable<Motorista> _motoristaList;


        public MotoristaTest()
        {
            _moqMotoristaRepository = new Mock<IRepositoryBase<Motorista>>();
            _moqCarroService        = new Mock<ICarroService>();
            _moqEnderecoService     = new Mock<IEnderecoService>();

            _motoristaService = new MotoristaService(_moqMotoristaRepository.Object, _moqCarroService.Object, _moqEnderecoService.Object);
        }

        [TestInitialize]
        public void Setup()
        {
            _motoristaList = new List<Motorista>
            {
                new Motorista {  PrimeiroNome = "josé", UltimoNome = "silva" },
                new Motorista {  PrimeiroNome = "joão", UltimoNome = "silva" }
            };
        }

        [TestMethod]
        public async Task Get_All()
        {
            _moqMotoristaRepository.Setup(repo => repo.GetAll())
                                   .Returns(Task.FromResult(_motoristaList));

            var motoristas = await _motoristaService.GetAll();

            Assert.AreEqual(2, motoristas.Count());
        }

        [TestMethod]
        public async Task Get_ByID()
        {
            var defaultMotorista = _motoristaList.FirstOrDefault();
            _moqMotoristaRepository.Setup(repo => repo.GetById(1))
                                   .Returns(Task.FromResult(defaultMotorista));

            var motorista = await _motoristaService.GetById(1);

            Assert.AreEqual(defaultMotorista, motorista);
        }

        [TestMethod]
        public async Task Add()
        {
            var defaultMotorista = _motoristaList.FirstOrDefault();
            _moqMotoristaRepository.Setup(repo => repo.Add(defaultMotorista))
                                   .Returns(Task.FromResult(defaultMotorista));

            var motorista = await _motoristaService.Add(defaultMotorista);

            Assert.AreEqual(defaultMotorista, motorista);
        }

    }
}

