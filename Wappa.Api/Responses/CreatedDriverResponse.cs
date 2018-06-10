using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Api.Models;

namespace Wappa.Api.Responses
{
    public class CreatedDriverResponse
    {
		public int Id { get; set; }

		public String FirstName { get; set; }

		public String LastName { get; set; }

		public Address Address { get; set; }

		public List<Car> Cars { get; set; }
	}
}
