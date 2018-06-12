using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Api.Responses
{
    public class UpdatedCarsResponse
    {
		public ICollection<Models.Car> Cars { get; set; }
	}
}
