using System.Collections.Generic;
using Newtonsoft.Json;

namespace WappaMobile.Driver.BackgroundTasks.DataTransferObject.Geocode
{
    public class GeolocationResponse
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }
}
