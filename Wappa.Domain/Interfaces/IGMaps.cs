using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Domain.Models;

namespace Wappa.Domain.Interfaces
{
    public interface IGMaps
    {
        Task<ValueObjectsGMaps> GetCoordinatesAsync(string address);
    }
}
