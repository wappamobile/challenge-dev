using System;
using System.Threading.Tasks;
using WappaMobile.Domain;

namespace WappaMobile.Application.Services.Geocoding
{
    /// <summary>
    /// Standard geocoding interface.
    /// A geocoder turns an address into latitude x longitude coordinates.
    /// </summary>
    public interface IGeocoder
    {
        /// <summary>
        /// Gets the lat/long coordinates for the given address.
        /// </summary>
        /// <returns>The lat/long coordinates for address.</returns>
        /// <param name="address">The address.</param>
        Task<Coordinates> GetCoordinatesForAddress(string address);
    }
}
