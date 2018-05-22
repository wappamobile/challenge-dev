using System;
using Newtonsoft.Json;

namespace Wappa.Core.Models
{
    public class Motorista 
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("PrimeiroNome")]
        public string PrimeiroNome { get; set; }

        [JsonProperty("UltimoNome")]
        public string UltimoNome { get; set; }

        [JsonProperty("Carro")]
        public Carro Carro { get; set; }

        [JsonProperty("Endereco")]
        public Endereco Endereco { get; set; }
    }
}