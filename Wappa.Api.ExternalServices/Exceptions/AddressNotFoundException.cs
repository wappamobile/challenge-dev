using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wappa.Api.ExternalServices.Exceptions
{
	public class AddressNotFoundException : Exception
	{
		private static String message;

		public AddressNotFoundException(String message) : base(message)	{	}

		public AddressNotFoundException(String address, IList<GoogleAddress> addresses) : base(message: message)
		{
			var messageBuilder = new StringBuilder($"I was unable to find a single place with this address: {address}");
			messageBuilder.AppendLine();
			messageBuilder.AppendLine("Found addresses: ");
			messageBuilder.AppendLine(this.FoundAddresses(addresses));

			message = messageBuilder.ToString();
		}

		private String FoundAddresses(IList<GoogleAddress> addresses)
		{
			return String.Join(Environment.NewLine, addresses.Select(a => a.FormattedAddress));
		}

	}
}
