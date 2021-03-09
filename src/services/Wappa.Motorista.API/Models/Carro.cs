using NSE.Core.DomainObjects;
using System;

namespace Wappa.Motorista.API.Models
{
	public class Carro: Entity
	{
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Placa { get; private set; }
        
        public Guid MotoristaId { get; private set; }

        // EF Relation
        public Motorista Motorista { get; protected set; }

        public Carro(string marca, string modelo, string placa)
        {
            Marca = marca;
            Modelo = modelo;
            Placa = placa;            
        }
    }
}
