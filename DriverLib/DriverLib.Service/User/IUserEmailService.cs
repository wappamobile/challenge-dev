using DriverLib.Domain;

namespace DriverLib.Service
{
    public interface IUserEmailService
    {
         void SendEmailForgotMyPasswordToUserAsync(User user);
    }
}
