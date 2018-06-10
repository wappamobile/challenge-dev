using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Api.Models
{
    public class Address
    {
		public int Id { get; set; }

		public String AddressLine { get; set; }

		public String City { get; set; }

		public Decimal Latitude { get; set; }

		public Decimal Longitude { get; set; }

		public String State { get; set; }

		public override string ToString()
		{
			return $"{AddressLine} {City} {State}";
		}
	}
}
