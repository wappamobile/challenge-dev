using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Api.Models;

namespace Wappa.Api.Responses
{
    public class DriverResponse
    {
		public int Id { get; set; }

		public String FirstName { get; set; }

		public String LastName { get; set; }

		public Address Address { get; set; }

		public List<Car> Cars { get; set; }
	}
}
