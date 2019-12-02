
using Newtonsoft.Json;

namespace Wappa.Teste.Project.Core.Models
{
    public class AddressModel
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [JsonProperty(PropertyName = "zip_code")]
        public string ZipCode { get; set; }
        public CoordinateModel Coordinate { get; set; }
    }
}
