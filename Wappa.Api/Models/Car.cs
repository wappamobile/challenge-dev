using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Api.Models
{
    public class Car
    {
		public int Id { get; set; }

		[Required]
		public String LicensePlate { get; set; }

		[Required]
		public String Model { get; set; }

		[Required]
		public String Vendor { get; set; }
	}
}
