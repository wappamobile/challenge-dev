using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Api.DomainModel
{
    public class Car
    {
		public int Id { get; set; }

		public Driver Driver { get; set; }

		public String LicensePlate { get; set; }

		public String Model { get; set; }

		public String Vendor { get; set; }
	}
}
