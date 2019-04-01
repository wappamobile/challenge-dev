using Wappa.Middleware.Domain.Common;

namespace Wappa.Middleware.Domain.Common
{
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
