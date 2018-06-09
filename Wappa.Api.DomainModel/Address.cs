using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Api.DomainModel
{
    public class Address
    {
		public int Id { get; set; }

		public String AddressLine { get; set; }

		public String City { get; set; }

		public Driver Driver { get; set; }

		public Decimal Latitude { get; set; }

		public Decimal Longitude { get; set; }

		public String State { get; set; }
	}
}
