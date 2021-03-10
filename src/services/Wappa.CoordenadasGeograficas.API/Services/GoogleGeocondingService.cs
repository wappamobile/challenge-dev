using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Wappa.CoordenadasGeograficas.API.Models;
using Wappa.Core.Extensions;

namespace Wappa.CoordenadasGeograficas.API.Services
{
	public interface IGoogleGeocondingService
	{
		public Task<Coordenadas> BuscarCoordenadas(Endereco endereco);
	}	

	public class GoogleGeocondingService : IGoogleGeocondingService
	{
		private readonly HttpClient _httpClient;
		private readonly string _key;

		public GoogleGeocondingService(HttpClient httpClient,
			IOptions<AppSettings> settings)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(settings.Value.GoogleGeocodingUrl);
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_key = settings.Value.GoogleAPiKey;
		}

		public async Task<Coordenadas> BuscarCoordenadas(Endereco endereco)
		{
			var response = await _httpClient.GetAsync($"?address={endereco.Consultar()}&key={_key}");
			var coordeandas = new Coordenadas();

			if (response.IsSuccessStatusCode)
			{
				string result = response.Content.ReadAsStringAsync().Result;

				var data = JsonConvert.DeserializeObject<dynamic>(result);
				
				foreach (var item in data.results)
				{
					coordeandas.Latitude = (decimal)(item.geometry.location.lat);
					coordeandas.Longitude = (decimal)(item.geometry.location.lng);

					if (coordeandas.EhValido())
						break;
				}
			}

			return coordeandas;
		}
	}
}
