using System;
using System.ComponentModel.DataAnnotations;

namespace Wappa.Api.Requests
{
	public class UpdateDriverAddressRequest
    {
		[Required]
		public String AddressLine { get; set; }

		[Required]
		public String PostalCode { get; set; }

		[Required]
		public String City { get; set; }

		[Required]
		public String State { get; set; }
	}
}
