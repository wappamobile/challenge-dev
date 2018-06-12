using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wappa.Api.Models
{
	public class Driver
    {
		[JsonIgnore]
		public int Id { get; set; }

		[Required]
		public String FirstName { get; set; }

		[Required]
		public String LastName { get; set; }

		[Required]
		public Address Address { get; set; }

		[Required]
		public List<Car> Car { get; set; }
	}
}
