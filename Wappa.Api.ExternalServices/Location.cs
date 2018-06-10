using Newtonsoft.Json;
using System;

namespace Wappa.Api.ExternalServices
{
	public class Location
	{
		[JsonProperty("lat")]
		public String Latitude { get; set; }

		[JsonProperty("lng")]
		public String Longitude { get; set; }
	}
}