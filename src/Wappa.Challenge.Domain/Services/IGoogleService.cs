using Wappa.Challenge.Domain.Commands.Inputs;

namespace Wappa.Challenge.Domain.Services
{
    public interface IGoogleService
    {
        (double? latitude, double? longitude) GetCoordinates(AddressCommand address);
    }
}