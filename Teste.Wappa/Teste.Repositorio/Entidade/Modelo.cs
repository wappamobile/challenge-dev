using System;
using System.Collections.Generic;
using System.Text;
using Teste.Repositorio.Interface;

namespace Teste.Repositorio.Entidade
{
    public class Modelo : IEntidade
    {
        public int? Id { get; set; }
        public string Descricao { get; set; }
        public Marca Marca { get; set; }
    }
}
