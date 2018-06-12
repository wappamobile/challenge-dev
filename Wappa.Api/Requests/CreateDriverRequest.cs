using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wappa.Api.Models;

namespace Wappa.Api.Requests
{
	public class CreateDriverRequest
    {
		[Required]
		public String FirstName { get; set; }

		[Required]
		public String LastName { get; set; }

		[Required]
		public Address Address { get; set; }

		[Required]
		public List<Car> Cars { get; set; }
	}
}
