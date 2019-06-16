using Flurl;
using Flurl.Http;
using Motoristas.Core;
using Motoristas.Core.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GoogleGeolocationService
{
    public class GeolocationService : IGeolocationService
    {
        private readonly ILogger _log;
        private readonly GoogleClientConfig _config;

        public GeolocationService(GoogleClientConfig config, ILogger log)
        {
            _config = config;
            _log = log.ForContext<GeolocationService>();
        }

        public Coordenadas ObterCoordenadas(string endereco)
        {
            object GetLogInfo(string enderecoRequest, long elapesedMilliseconds, bool b)
            {
                return new
                {
                    Url = _config.UriAddress,
                    TempoConsulta = elapesedMilliseconds,
                    Status = b ? "Sucesso" : "Erro",
                    Parametros = new
                    {
                       Address = enderecoRequest
                    }
                };
            }

            var sw = Stopwatch.StartNew();
            const string msg = "Consulta Google GeoCoding {@ConsultaGoogle} encerrada.";

            try
            {
                var data = GetGoogleGeocoding(endereco).Result;
                sw.Stop();
                if (data != null)
                {
                    var logInfo = GetLogInfo(endereco, sw.ElapsedMilliseconds, true);
                    _log.Information(msg, logInfo);
                    return new Coordenadas(data.RootObjetc.results[0].geometry.location.lat, data.RootObjetc.results[0].geometry.location.lng);
                }
                else
                {
                    var logInfo = GetLogInfo(endereco, sw.ElapsedMilliseconds, false);
                    _log.Information(msg, logInfo);
                    return null;
                }
                
            }
            catch(Exception ex)
            {
                sw.Stop();
                var logInfo = GetLogInfo(endereco, sw.ElapsedMilliseconds, false);
                _log.Error(ex, msg, logInfo);
                var e = new Exception($"Google geocoding: Erro ao consultar informações (Tempo decorrido: {sw.ElapsedMilliseconds:000}ms).", ex);
                throw e;
            }
        }

        private async Task<MapsResponseDto> GetGoogleGeocoding(string address)
        {
            var result = await _config.UriAddress
                        .SetQueryParam("address", address)
                        .SetQueryParam("key", _config.AccessKey)
                        .GetJsonAsync<MapsResponseDto>();

            _log.Debug("Result {@Result} obtido {Address}", result, address);

            return result;
           
        }

        private class MapsResponseDto
        {
            public Rootobject RootObjetc { get; set; }

            public class Rootobject
            {
                public Result[] results { get; set; }
                public string status { get; set; }
            }

            public class Result
            {
                public Address_Components[] address_components { get; set; }
                public string formatted_address { get; set; }
                public Geometry geometry { get; set; }
                public string place_id { get; set; }
                public string[] types { get; set; }
            }

            public class Geometry
            {
                public Location location { get; set; }
                public string location_type { get; set; }
                public Viewport viewport { get; set; }
            }

            public class Location
            {
                public float lat { get; set; }
                public float lng { get; set; }
            }

            public class Viewport
            {
                public Northeast northeast { get; set; }
                public Southwest southwest { get; set; }
            }

            public class Northeast
            {
                public float lat { get; set; }
                public float lng { get; set; }
            }

            public class Southwest
            {
                public float lat { get; set; }
                public float lng { get; set; }
            }

            public class Address_Components
            {
                public string long_name { get; set; }
                public string short_name { get; set; }
                public string[] types { get; set; }
            }

        }
    }
}
