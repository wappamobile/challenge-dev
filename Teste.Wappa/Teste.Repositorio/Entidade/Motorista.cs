using System;
using System.Collections.Generic;
using System.Text;
using Teste.Repositorio.Interface;

namespace Teste.Repositorio.Entidade
{
    public class Motorista : IEntidade {
        public int? ID { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public Endereco Endereco { get; set; }
        public Carro Carro { get; set; }
    }

}
