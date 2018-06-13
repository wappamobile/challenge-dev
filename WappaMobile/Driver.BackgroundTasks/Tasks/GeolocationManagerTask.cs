using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WappaMobile.Driver.API.Model;
using WappaMobile.Driver.BackgroundTasks.DataTransferObject.Geocode;
using WappaMobile.Driver.BackgroundTasks.Tasks.Base;
using WappaMobile.Driver.Infrastructure.Repositories;

namespace WappaMobile.Driver.BackgroundTasks.Tasks
{
    public class GeolocationManagerTask : BackgroundService
    {
        private readonly BackgroundTaskSettings _settings;
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        private HttpClient _client;

        public GeolocationManagerTask(
            IOptions<BackgroundTaskSettings> settings,
            IDriverRepository driverRepository,
            IMapper mapper)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _driverRepository = driverRepository ?? throw new ArgumentNullException(nameof(driverRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _client = new HttpClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                FetchDriversLocation().Wait();

                await Task.Delay(_settings.TaskDelay, stoppingToken);
            }

            await Task.CompletedTask;
        }

        private async Task FetchDriversLocation()
        {
            var drivers = _driverRepository.GetPendingGeolocation();

            foreach (var driver in drivers)
            {
                if (!string.IsNullOrEmpty(driver.Address))
                {
                    var results = await RequestGeolocation(driver.Address);

                    if (results != null)
                    {
                        driver.Geolocation = _mapper.Map<List<Result>, List<Geolocation>>(results.Results);
                        driver.FetchGeolocation = false;
                        _driverRepository.Update(driver);
                    }
                }
                else
                {
                    driver.FetchGeolocation = false;
                    _driverRepository.Update(driver);
                }
            }
        }

        private async Task<GeolocationResponse> RequestGeolocation(string search)
        {
            var formattedUrl = string.Format(_settings.GeocodeUrl, search, _settings.GeocodeKey);

            var response = await _client.GetAsync(formattedUrl);

            return await response.Content.ReadAsJsonAsync<GeolocationResponse>();
        }
    }

    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string body = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
