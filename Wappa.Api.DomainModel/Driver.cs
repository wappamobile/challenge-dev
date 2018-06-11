using System;
using System.Collections.Generic;

namespace Wappa.Api.DomainModel
{
    public class Driver
    {
		public int Id { get; set; }

		public String FirstName { get; set; }

		public String LastName { get; set; }

		public Address Address { get; set; }

		public ICollection<Car> Cars { get; set; }
	}
}
