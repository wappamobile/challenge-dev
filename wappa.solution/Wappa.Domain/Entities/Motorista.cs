using System;
using System.Collections.Generic;
using System.Linq;


namespace Wappa.Domain.Entities
{
    public class Motorista
    {
        public int ID {get;set;}
        public string PrimeiroNome {get;set;}
        public string UltimoNome {get;set;}
        
        public Carro Carro {get;set;}
        public Endereco Endereco{get;set;}
    }
}