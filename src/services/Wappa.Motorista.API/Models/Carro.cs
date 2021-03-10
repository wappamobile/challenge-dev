﻿using Wappa.Core.DomainObjects;
using System;

namespace Wappa.Motoristas.API.Models
{
	public class Carro: Entity
	{
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        
        public Guid MotoristaId { get; set; }

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
