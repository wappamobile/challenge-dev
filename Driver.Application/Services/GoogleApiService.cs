using Driver.Application.Data.Entities;
using Driver.Application.Services.Entities;
using Driver.Application.Services.Interfaces;
using Driver.Application.Util;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Application.Services
{
    /// <summary>
    /// Serviço de busca na api de mapas do Google
    /// </summary>
    public class GoogleApiService : IGoogleApiService
    {
        private readonly static Uri BaseUri = new Uri("https://maps.googleapis.com/maps/api/geocode/json");

        /// <summary>
        /// Método responsável por buscar um endereço baseado na entidade informada
        /// </summary>
        /// <param name="addressEntity"></param>
        /// <returns></returns>
        public async Task<GoogleMapsResult> SearchAsync(AddressEntity addressEntity)
        {
            UriBuilder builder = new UriBuilder(BaseUri)
            {
                Query = UriUtil.ToQueryString(("address", BuildAddressQuery(addressEntity)), ("key", AppSettings.GoogleApiKey))
            };

            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(builder.Uri))
                {
                    if(response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<GoogleMapsResult>(json);
                    }

                    return null;
                }
            }
        }

        /// <summary>
        /// Método responsável por montar o endereço com as informações que tiver
        /// </summary>
        /// <param name="addressEntity"></param>
        /// <returns></returns>
        public static string BuildAddressQuery(AddressEntity addressEntity)
        {
            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(addressEntity.Address))
                builder.Append(addressEntity.Address);

            if (!string.IsNullOrWhiteSpace(addressEntity.AddressNumber))
            {
                if (builder.Length > 0)
                    builder.Append(", ");

                builder.Append(addressEntity.AddressNumber);
            }

            bool traceAdded = false;

            if (!string.IsNullOrWhiteSpace(addressEntity.District))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" - ");
                    traceAdded = true;
                }

                builder.Append(addressEntity.District);
            }

            if (!string.IsNullOrWhiteSpace(addressEntity.City))
            {
                if (builder.Length > 0)
                {
                    if (!traceAdded)
                    {
                        builder.Append(" - ");
                        traceAdded = true;
                    }
                    else
                        builder.Append(", ");

                }

                builder.Append(addressEntity.City);
            }

            if (!string.IsNullOrWhiteSpace(addressEntity.State))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" - ");
                }

                builder.Append(addressEntity.State);
            }

            return System.Net.WebUtility.UrlEncode(builder.ToString());
        }
    }
}