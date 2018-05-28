using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Dominio.Entidade;
using Wappa.Dominio.Resultado;
using Wappa.Dominio.Servico;
using Wappa.Util;

namespace Wappa.Infraestrutura.Servico
{
    public class GoogleMap : IGoogleMap
    {
        public ResuladoGoogleMap ObterLocalidade(Endereco endereco)
        {
            var client = new HttpClientUtil<ResuladoGoogleMap>();

            var enderecoCompleto = $"{endereco.Logradouro}, {endereco.Cep}";

            string parametro = $@"json?address={enderecoCompleto}&key=AIzaSyDLfGROG0b8N1tD-DPm8oxaRCkECAlZPMo";

            var resultado = client.Get("https://maps.googleapis.com/maps/api/geocode", parametro);

           return  resultado.Result;
        }
    }
}
