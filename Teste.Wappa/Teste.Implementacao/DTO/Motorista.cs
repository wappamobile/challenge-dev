using System;
using System.Collections.Generic;
using System.Text;
using Teste.Implementacao.Inteface;
using Teste.Repositorio.DTO.Extensoes;

namespace Teste.Implementacao.DTO
{
    public class Motorista : IDTO
    {
        public Motorista(int? id, string nome, string sobrenome)
        {
            this.Id = id;
            this.Nome = nome.ValidaAtributo(nameof(nome));
            this.Sobrenome = sobrenome.ValidaAtributo(nameof(sobrenome));
        }

        public int? Id { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public Carro Carro { get; set; }
        public Endereco Endereco { get; set; }
    }
}
