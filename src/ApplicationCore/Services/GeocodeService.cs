using ApplicationCore.Entity;
using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services.APIClient.Geocode.Response;
using Flurl.Http;
using Flurl.Http.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class GeocodeService : IGeocodeService
    {
        private const string _URL = "https://maps.googleapis.com/maps/api/geocode/json";
        private const string _KEY = "AIzaSyDwrL7Ax6ljInjzCjkCyqY7EE2I0l8rXfk";

        private readonly IFlurlClient _flurlClient;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IGeoLocationRepository _geoLocationRepository;

        public GeocodeService(IFlurlClientFactory flurlClientFac,
            IEnderecoRepository enderecoRepository,
            IGeoLocationRepository geoLocationRepository)
        {
            this._flurlClient = flurlClientFac.Get(_URL);
            this._enderecoRepository = enderecoRepository;
            this._geoLocationRepository = geoLocationRepository;
        }

        public void SetGeometryAsync(int enderecoId)
        {
            var endereco = _enderecoRepository.GetById(enderecoId);
            if (endereco == null)
                return;

            var result = GetGeometryAsync(getFormatedAddress(endereco)).Result;

            if (result.Status == "OK" && result.Results.Any())
            {
                var geometry = result.Results.FirstOrDefault().Geometry;
                if (geometry != null && geometry.Location != null)
                {
                    var geoLocation = saveGeoLocation(endereco, geometry.Location.Lat, geometry.Location.Lng);
                    updateEndereco(endereco, geoLocation.GeoLocationId);
                }

            }
        }

        private async Task<GeocodeAddressResponse> GetGeometryAsync(string endereco)
        {
            return await _flurlClient
                .Request()
                .SetQueryParams(new { key = _KEY, address = endereco })
                .GetAsync()
                .ReceiveJson<GeocodeAddressResponse>();
        }

        private string getFormatedAddress(Endereco endereco)
        {
            return $"{endereco.Logradouro}, {endereco.Numero} - {endereco.Cidade} - {endereco.UF}, {endereco.CEP}";
        }

        private GeoLocation saveGeoLocation(Endereco endereco, double latitude, double longitude)
        {
            GeoLocation geoLocation = null;

            if (endereco.GeoLocationId.HasValue)
            {
                geoLocation = _geoLocationRepository.GetById(endereco.GeoLocationId.Value);
            }

            if (geoLocation == null)
            {
                geoLocation = new GeoLocation(latitude, longitude);
                _geoLocationRepository.Add(geoLocation);
            }
            else
            {
                _geoLocationRepository.Update(geoLocation);
            }

            _geoLocationRepository.SaveChanges();

            return geoLocation;
        }

        private void updateEndereco(Endereco endereco, int geoLocationId)
        {
            endereco.GeoLocationId = geoLocationId;
            _enderecoRepository.Update(endereco);
            _enderecoRepository.SaveChanges();
        }
    }
}
