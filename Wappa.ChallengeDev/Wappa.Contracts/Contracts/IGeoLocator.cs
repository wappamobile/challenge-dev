using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Contracts.Models;

namespace Wappa.Contracts
{
    public interface IGeoLocator
    {
        Task<Location> GetLocation(IConfiguration configuration, string address);
    }
}
