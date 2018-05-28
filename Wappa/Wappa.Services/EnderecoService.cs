using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Wappa.Core.Common;
using Wappa.Core.Repositories;
using Wappa.Core.Services;
using Wappa.Entities;
using Wappa.Services.Interfaces;
using static Wappa.Entities.Coordenadas;

namespace Wappa.Services
{
    public class EnderecoService : ServiceBase<Endereco>, IEnderecoService
    {
        private readonly IConfiguration _configuration;

        public EnderecoService(IRepositoryBase<Endereco> repository,
                               IConfiguration configuration ) : base(repository)
        {
            _configuration = configuration;
        }

        public override async Task<Endereco> Add(Endereco obj)
        {
            var coordenada = await GetCoordenadas(obj);
            if (coordenada != null)
            {
                obj.Latitude  = coordenada.Lat;
                obj.Longitude = coordenada.Lng;
            }

            return await base.Add(obj);
        }

        private async Task<Location> GetCoordenadas(Endereco model)
        {
            var requestUri = string.Format("?address={0}&key={1}",
                (model.Numero     + "+" + 
                 model.Logradouro + ",+" + 
                 model.Bairro     + ",+" + 
                 model.Estado).Replace(" ", "+"),
                _configuration["Google:GeoKey"]);

            HttpResponseMessage response = WebApiHelper.WebApiClient(_configuration["Google:GeoURL"])
                                                       .GetAsync(requestUri)
                                                       .Result;


            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var geoResult = JsonConvert.DeserializeObject<RootObject>(result);

                if (geoResult.Results != null && geoResult.Results.Count > 0)
                {
                    return geoResult?.Results?.FirstOrDefault().Geometry?.Location;
                }
            }

            return null;
        }

    }
}
