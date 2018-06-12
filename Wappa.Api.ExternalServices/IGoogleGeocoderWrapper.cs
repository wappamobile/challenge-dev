using System.Threading.Tasks;

namespace Wappa.Api.ExternalServices
{
	public interface IGoogleGeocoderWrapper
	{
		Task<GoogleAddress> GetAddress(string address);
	}
}