using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Xml.Linq;
using TesteDev.Servicos.Entidades;
using TesteDev.Servicos.Interfaces;
using TesteDev.Servicos.ModelsConfiguracao;

namespace TesteDev.Servicos
{
    public class LocalizacaoServico : ILocalizacaoServico
    {
        private readonly ConfiguracaoCoordenadas _configuracaoCoordenadas;

        /// <summary>
        /// Contrutor com configurações de serviço externo (DI)
        /// </summary>
        /// <param name="configuracaoCoordenadas">Condigurações para consultar serviço externo</param>
        public LocalizacaoServico(IOptions<ConfiguracaoCoordenadas> configuracaoCoordenadas)
        {
            _configuracaoCoordenadas = configuracaoCoordenadas.Value;
        }

        /// <summary>
        /// Método responsável por retornar a localizção geográfica de um endereço
        /// </summary>
        /// <param name="endereco">Endereço a ser consultado</param>
        /// <returns>Objeto com as informações encontradas</returns>
        public Localizacao RetornarLocalizacao(string endereco)
        {
            Localizacao retorno = null;

            try
            {
                string requestUri = _configuracaoCoordenadas.UrlAPI.Replace("{0}", Uri.EscapeDataString(endereco));

                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = request.GetResponse();
                XDocument xdoc = XDocument.Load(response.GetResponseStream());

                XElement result = xdoc.Element("GeocodeResponse").Element("result");

                if(result != null)
                {
                    XElement localizacao = result.Element("geometry").Element("location");
                    XElement lat = localizacao.Element("lat");
                    XElement lng = localizacao.Element("lng");

                    retorno = new Localizacao();
                    retorno.Latitude = lat.Value;
                    retorno.Longitude = lng.Value;
                }
            }
            catch (Exception)
            {
                //Tratar possíveis erros de uso da API
                throw;
            }

            return retorno;
        }
    }
}
