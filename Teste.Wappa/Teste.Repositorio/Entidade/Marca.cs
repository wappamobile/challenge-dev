using System;
using System.Collections.Generic;
using System.Text;
using Teste.Repositorio.Interface;

namespace Teste.Repositorio.Entidade
{
    public class Marca: IEntidade
    {
        public int? Id { get; set; }
        public string Descricao { get; set; }
    }
}
