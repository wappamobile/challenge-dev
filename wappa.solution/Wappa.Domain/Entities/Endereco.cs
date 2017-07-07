using System;
using System.Collections.Generic;
using System.Linq;


namespace Wappa.Domain.Entities
{
    public class Endereco
    {
        public int ID {get;set;}
        public string CEP {get;set;}
        public string Logradouro {get;set;}
        public int Numero {get;set;}
        public string Complemento{get;set;}
        public string Bairro{get;set;}
        public string Cidade {get;set;}
        public string Estado{get;set;}

		public Double Latitude {get; set; }
		public Double Longitude {get; set; }        
    }
}