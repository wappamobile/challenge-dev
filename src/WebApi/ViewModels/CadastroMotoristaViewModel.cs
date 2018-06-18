using ApplicationCore.Entity;
using Newtonsoft.Json;
using System;

namespace WebApi.ViewModels
{
    public class CadastroMotoristaViewModel
    {
        [JsonProperty("motoristaId")]
        public int MotoristaId { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("sobrenome")]
        public string Sobrenome { get; set; }

        [JsonProperty("marcaCarro")]
        public string MarcaCarro { get; set; }
        [JsonProperty("modeloCarro")]
        public string ModeloCarro { get; set; }
        [JsonProperty("placaCarro")]
        public string PlacaCarro { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }
        [JsonProperty("numero")]
        public int Numero { get; set; }
        [JsonProperty("complemento")]
        public string Complemento { get; set; }
        [JsonProperty("cep")]
        public string CEP { get; set; }
        [JsonProperty("cidade")]
        public string Cidade { get; set; }
        [JsonProperty("uf")]
        public string UF { get; set; }
        [JsonProperty("latitude")]
        public double? Latitude { get; set; }
        [JsonProperty("longitude")]
        public double? Longitude { get; set; }
        [JsonProperty("datacadastro")]
        public DateTime DataCadastro { get; set; }

        public CadastroMotoristaViewModel(Motorista motorista)
        {
            this.MotoristaId = motorista.MotoristaId;
            this.Nome = motorista.Nome;
            this.Sobrenome = motorista.Sobrenome;
            this.MarcaCarro = motorista.Carro.Marca;
            this.ModeloCarro = motorista.Carro.Modelo;
            this.PlacaCarro = motorista.Carro.Placa;
            this.Logradouro = motorista.Endereco.Logradouro;
            this.Numero = motorista.Endereco.Numero;
            this.Complemento = motorista.Endereco.Complemento;
            this.CEP = motorista.Endereco.CEP;
            this.Cidade = motorista.Endereco.Cidade;
            this.UF = motorista.Endereco.UF;
            this.Latitude = motorista.Endereco.GeoLocation?.Latitude;
            this.Longitude = motorista.Endereco.GeoLocation?.Longitude;
            this.DataCadastro = motorista.DataCadastro;
        }
    }
}
