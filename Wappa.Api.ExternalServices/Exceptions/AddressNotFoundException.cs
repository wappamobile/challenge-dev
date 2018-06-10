using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Api.ExternalServices.Exceptions
{
	public class AddressNotFoundException : Exception
	{
		public AddressNotFoundException(string message) : base(message)
		{
		}
	}
}
