using System;
using System.Collections.Generic;
using System.Text;
using MG.WappaDriverAPI.Core.Models;

namespace MG.WappaDriverAPI.Core.Services.Interfaces
{
    public interface IGoogleApiMapsService
    {
        GoogleAddress GetAddressFromGoogle(string address);
    }
}
