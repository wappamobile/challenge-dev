using Challenge.Dev.Models;
using System.Collections.Generic;

namespace Challenge.Dev.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsers(SortingParams sortingParams);
    }
}
