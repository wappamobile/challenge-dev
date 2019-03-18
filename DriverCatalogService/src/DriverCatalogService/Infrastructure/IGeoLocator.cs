using DriverCatalogService.Models;

namespace DriverCatalogService.Infrastructure
{
    public interface IGeoLocator
    {
        Address LocateAddress(string addressFullAddress);
    }
}