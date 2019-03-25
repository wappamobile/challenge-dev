using DriverLib.Domain;
using System.Threading.Tasks;

namespace DriverLib.Repository
{
    public interface IUserRepository : IRepositoryGeneric<User>
    {
        Task<User> UpdatePassword(User user);
    }
}
