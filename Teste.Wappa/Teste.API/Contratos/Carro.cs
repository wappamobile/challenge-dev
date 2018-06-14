using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.API.Contratos
{
    public class Carro
    {
        public int? Id { get; set; }
        public int? IdMarca { get; set; }
        public string Marca { get; set; }
        public int? IdModelo { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }
}
