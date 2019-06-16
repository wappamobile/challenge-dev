using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motoristas.Core.Data.MongoDB
{
    public interface IDbContext<T>
    {
        IQueryable<T> Collection { get; }
        T Load(string id);
        Task Save(T model);
        T Delete(string id);
        List<T> Find(string sort);
    }
}
