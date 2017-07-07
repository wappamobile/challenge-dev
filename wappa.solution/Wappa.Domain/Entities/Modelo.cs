using System;
using System.Collections.Generic;
using System.Linq;


namespace Wappa.Domain.Entities
{
    public class Modelo
    {
        public int ID {get;set;}

        public Marca Marca {get;set;}
        public string Descricao {get;set;}
    }
}