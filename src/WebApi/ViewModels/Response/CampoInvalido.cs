using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels.Response
{
    public class CampoInvalido
    {
        [JsonProperty("campo")]
        public string Campo { get; set; }
        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }
    }
}
