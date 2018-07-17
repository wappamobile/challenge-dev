using System;
using System.Collections.Generic;

namespace DriverRegistration.Domain.Interfaces
{
    public interface IExternalApiGeocoding
    {
        IDictionary<String, Decimal> GetGeoCordinates(String address);
    }
}
