using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Interfaces.Services;
using System;

namespace ApplicationCore.Services
{
    public class CarroService : ICarroService
    {
        private readonly ICarroRepository _carroRepository;

        public CarroService(ICarroRepository carroRepository)
        {
            this._carroRepository = carroRepository;
        }

        public void Add(Carro carro)
        {
            carro.DataCadastro = DateTime.Now;
            carro.Ativo = true;
            _carroRepository.Add(carro);
            _carroRepository.SaveChanges();
        }

        public void Update(Carro carro)
        {
            if(carro == null)
                throw new CarroNaoEncontradoException();

            var carroDb = this._carroRepository.GetById(carro.CarroId);
            if (carroDb == null)
                throw new CarroNaoEncontradoException();

            carroDb.Marca = carro.Marca;
            carroDb.Modelo = carro.Modelo;
            carroDb.Placa = carro.Placa;

            _carroRepository.Update(carroDb);
            _carroRepository.SaveChanges();
        }
    }
}
