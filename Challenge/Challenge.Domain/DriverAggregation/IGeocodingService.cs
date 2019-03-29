using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Domain.DriverAggregation
{
    public interface IGeocodingService
    {
        string GetGeocodingByAddress(string address);
    }
}
