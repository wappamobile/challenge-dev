using System;
using System.ComponentModel.DataAnnotations;

namespace Wappa.Api.Requests
{
	public class UpdateDriverCarRequest
    {
		[Required]
		public String LicensePlate { get; set; }

		[Required]
		public String Model { get; set; }

		[Required]
		public String Vendor { get; set; }
	}
}
