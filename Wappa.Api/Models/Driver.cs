using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Api.Models
{
    public class Driver
    {
		public int Id { get; set; }

		public Address Address { get; set; }

		public List<Car> Car { get; set; }

		public String FirstName { get; set; }

		public String LastName { get; set; }
	}
}
