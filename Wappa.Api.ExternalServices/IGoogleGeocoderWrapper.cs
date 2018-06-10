using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wappa.Api.ExternalServices
{
	public interface IGoogleGeocoderWrapper
	{
		Task<IList<GoogleAddress>> GetAddress(string address);
	}
}