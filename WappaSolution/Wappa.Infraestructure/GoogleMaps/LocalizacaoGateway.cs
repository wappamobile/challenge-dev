using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Wappa.Domain.Services;

namespace Wappa.Infraestructure.GoogleMaps
{
    public class LocalizacaoGateway : ILocalizacaoGateway
    {
        private readonly GoogleMapsConfiguration configuration;

        public LocalizacaoGateway(IOptions<GoogleMapsConfiguration> configuration)
        {
            this.configuration = configuration.Value;
        }

        public Dictionary<string, string> ObterCoordenadas(string endereco)
        {
            var result = new Dictionary<string, string>();

            try
            {
                var requestUri = String.Format(configuration.UrlAPI, new string[] { Uri.EscapeDataString(endereco), configuration.KeyAPI });

                var request = WebRequest.Create(requestUri);
                var response = request.GetResponse();
                var xdoc = XDocument.Load(response.GetResponseStream());

                var resultXElem = xdoc.Element("GeocodeResponse").Element("result");

                if(resultXElem != null)
                {
                    var locationXElem = resultXElem.Element("geometry").Element("location");

                    result.Add("lat", locationXElem.Element("lat").Value);
                    result.Add("lng", locationXElem.Element("lng").Value);
                }
                else
                {
                    throw new Exception("Endereço não localizado.");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }
    }
}
