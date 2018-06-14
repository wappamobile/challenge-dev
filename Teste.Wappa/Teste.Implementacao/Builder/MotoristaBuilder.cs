using System;
using System.Collections.Generic;
using System.Text;
using Teste.Implementacao.DTO;
using Teste.Implementacao.DTO.Extensao;

namespace Teste.Implementacao.Builder
{
    public class MotoristaBuilder
    {
        private Motorista motorista;

        public MotoristaBuilder()
        {
        }

        public MotoristaBuilder Novo(int? id, string nome, string sobrenome)
        {
            motorista = new Motorista(id, nome, sobrenome);
            return this;
        }

        public MotoristaBuilder ComCarro(int? id, int? idMarca, string marca, int? idModelo, string modelo, string placa)
        {
            motorista.Carro = new Carro(id, idMarca, marca, idModelo, modelo, placa);
            return this;
        }

        public MotoristaBuilder ComEndereco(int? id, string logradouro, string numero, string complemento, string bairro, string cidade, string estado, int cep, double? latitude, double? longitude)
        {
            motorista.Endereco = new Endereco(id, logradouro, numero, complemento, bairro, cidade, estado, cep, latitude, longitude);
            return this;
        }

        private Exception ObterException()
        {
            return new Exception("O motorista precisa ser criado.");
        }

        public Motorista Criar()
        {
            return motorista;
        }

    }
}
