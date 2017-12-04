using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Domain.Entities;

namespace Wappa.WebApi.ViewModels.Common
{
    public class MotoristaViewModel
    {
        public int MotoristaId { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public CarroViewModel Carro { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public CidadeViewModel Cidade { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public MotoristaViewModel(Motorista motorista)
        {
            this.MotoristaId = motorista.MotoristaId;
            this.Nome = motorista.Nome;
            this.Sobrenome = motorista.Sobrenome;
            this.Carro = new CarroViewModel(motorista.Carro);
            this.Logradouro = motorista.Logradouro;
            this.Numero = motorista.Numero;
            this.Bairro = motorista.Bairro;
            this.Cep = motorista.Cep;
            this.Cidade = new CidadeViewModel(motorista.Cidade);
            this.Latitude = motorista.Latitude;
            this.Longitude = motorista.Longitude;
        }
    }
}
