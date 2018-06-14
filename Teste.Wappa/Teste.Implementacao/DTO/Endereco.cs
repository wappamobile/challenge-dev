using System;
using System.Collections.Generic;
using System.Text;
using Teste.Implementacao.Inteface;
using Teste.Repositorio.DTO.Extensoes;

namespace Teste.Implementacao.DTO
{
    public class Endereco : IDTO
    {
        public Endereco(int? id, string logradouro, string numero, string complemento, string bairro, string cidade, string estado, int cep, double? latitude, double? longitude)
        {
            this.Id = id;
            this.Logradouro = logradouro.ValidaAtributo(nameof(logradouro));
            this.Numero = numero.ValidaAtributo(nameof(numero));
            this.Bairro = bairro.ValidaAtributo(nameof(bairro));
            this.Complemento = complemento;
            this.Cidade = cidade.ValidaAtributo(nameof(cidade));
            this.Estado = estado.ValidaAtributo(nameof(estado));
            this.CEP = cep.ValidaAtributo(nameof(cep));
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public void InformarCoordenadas(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public int? Id { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public int CEP { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }
    }

}
