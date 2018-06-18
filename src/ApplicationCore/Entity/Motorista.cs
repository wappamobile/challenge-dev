using System;

namespace ApplicationCore.Entity
{
    public class Motorista
    {
        public int MotoristaId { get; set; }
        public int CarroId { get; set; }
        public int EnderecoId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        public Carro Carro { get; set; }
        public Endereco Endereco { get; set; }

    }
}
