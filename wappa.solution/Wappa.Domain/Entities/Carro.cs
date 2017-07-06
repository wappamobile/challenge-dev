using System;
using System.Collections.Generic;
using System.Linq;


namespace Wappa.Domain.Entities
{
    public class Carro
    {
        public int ID {get;set;}
        public Modelo Modelo {get;set;}
        public string Placa {get;set;}
    }
}