using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Api.Models
{
    public class Car
    {
		public int Id { get; set; }

		public String LicensePlate { get; set; }

		public String Model { get; set; }

		public String Vendor { get; set; }
	}
}
