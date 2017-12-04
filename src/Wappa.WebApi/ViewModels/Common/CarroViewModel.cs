using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Domain.Entities;

namespace Wappa.WebApi.ViewModels.Common
{
    public class CarroViewModel
    {
        public int CarroId { get; private set; }
        public string Marca { get; private set; }
        public string Placa { get; private set; }

        public CarroViewModel(Carro carro)
        {
            this.CarroId = carro.CarroId;
            this.Marca = carro.Marca;
            this.Placa = carro.Placa;
        }
    }
}
