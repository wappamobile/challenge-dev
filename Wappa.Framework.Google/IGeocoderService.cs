using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Framework.Google.Geocoding.Core;

namespace Wappa.Framework.Google.Geocoding
{
    public interface IGeocoderService
    {
        Task<IEnumerable<Address>> GeocodeAsync(string address);
        Task<IEnumerable<Address>> GeocodeAsync(string street, string city, string state, string postalCode, string country);
    }
}
