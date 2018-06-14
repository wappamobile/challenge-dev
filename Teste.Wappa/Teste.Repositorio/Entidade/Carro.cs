using System;
using System.Collections.Generic;
using System.Text;
using Teste.Repositorio.Interface;

namespace Teste.Repositorio.Entidade
{
    public class Carro : IEntidade
    {
        public int? Id { get; set; }
        public int? IdMotorista { get; set; }
        public Modelo Modelo { get; set; }
        public string Placa { get; set; }
    }
}
