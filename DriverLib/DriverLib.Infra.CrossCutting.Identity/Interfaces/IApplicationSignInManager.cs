using DriverLib.Domain;
using DriverLib.Infra.CrossCutting.Identity;

namespace DriverLib.Infra.CrossCutting.Identity.Interfaces
{
    public interface IApplicationSignInManager
    {
        object GenerateTokenAndSetIdentity(User user, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations);
    }
}
