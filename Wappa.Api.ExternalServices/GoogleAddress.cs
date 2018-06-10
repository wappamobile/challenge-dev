using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Api.ExternalServices
{
	public class GoogleAddress
    {
		[JsonProperty("formatted_address")]
		public String FormattedAddress { get; set; }

		
		public Location Location { get; set; }

		[JsonProperty("place_id")]
		public String PlaceId { get; set; }

	}
}
