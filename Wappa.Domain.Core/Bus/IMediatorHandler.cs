using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Domain.Core.Commands;
using Wappa.Domain.Core.Events;

namespace Wappa.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
