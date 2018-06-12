using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Challenge.ApplicationCore.Entities;
using Wappa.Challenge.ApplicationCore.Interfaces.Repositories;
using Wappa.Challenge.ApplicationCore.Interfaces.Services;

namespace Wappa.Challenge.ApplicationCore.Services
{
    public class CarroService : ABaseService<Carro>, ICarroService
    {
        private readonly ICarroRepository _carroRepository;
        public CarroService(ICarroRepository carroRepository_) : base(carroRepository_)
        {
            _carroRepository = carroRepository_;
        }

        public virtual bool Apagar(long id)
        {
            return _carroRepository.Apagar(id);
        }
    }
}
