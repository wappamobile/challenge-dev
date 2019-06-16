using System.Collections.Generic;
using MediatR;

namespace Motoristas.Core
{
    public interface IEntity<out TId>
    {
        TId Id { get; }
    }
}
