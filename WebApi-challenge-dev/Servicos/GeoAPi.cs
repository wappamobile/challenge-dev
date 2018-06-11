using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using WebApi_challengedev.Data;

namespace WebApi_challengedev.Servicos
{
    public class GeoAPi
    {
        private IConfiguration configuration;
        private string APIBody { get; set; }
        private string APIKey { get; set; }
        public GoogleGeoApi _googleGeo { get; set; }

        //Construtor
        public GeoAPi(IConfiguration config)
        {
            configuration = config;
            APIBody = configuration.GetConnectionString("GoogleApiBody");
            APIKey = configuration.GetConnectionString("GoogleApiKey");
        }


        //Requisitor de GeoLocolizacao
        public void ResquestGeoApi(Motoristas Moto)
        {

            //Mount da Request
            string endereco = Moto.DadosEndereco.Numero.ToString()+"+" +Moto.DadosEndereco.Rua.Replace(" ","+")+ ",+" + Moto.DadosEndereco.Cidade.Replace(" ", "+") + ",+" + Moto.DadosEndereco.Estado;
            StringBuilder sb = new StringBuilder();
            string enderecoUrl = sb.AppendFormat("{0}address={1}&key={2}", APIBody, endereco, APIKey).ToString();
            WebRequest GETURL = WebRequest.Create(enderecoUrl);
            GoogleGeoApi GeoApi = new GoogleGeoApi();
            JsonSerializer serializer = new JsonSerializer();

            try
            {
                using (Stream stream = GETURL.GetResponse().GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        var jsonTextReader = new JsonTextReader(sr);
                        GeoApi = serializer.Deserialize<GoogleGeoApi>(jsonTextReader);
                    }

                };

               
            }
            catch (Exception e)
            {
                GeoApi.status = $"ERRO: {e.Message}";

            }

            _googleGeo = GeoApi;

        }

       
        //Set GeoLocation Motorista
        public string SetGeoMotorista(Motoristas moto, GoogleGeoApi geoApi, out Motoristas motoOut)
        {
            string result = string.Empty;

            try
            {
                if (!geoApi.status.Contains("ERRO"))
                {
                    moto.DadosEndereco.Lat = geoApi.results[0].geometry.location.lat;
                    moto.DadosEndereco.Log = geoApi.results[0].geometry.location.lng;
                    result = $"Sucesso: Cadastro de Geolocations: lat { moto.DadosEndereco.Lat} // lng {moto.DadosEndereco.Log}";
                }

                motoOut = moto;
                return result;
            }
            catch
            {
                motoOut = moto;
                return result;
            }

        }

    }
}
