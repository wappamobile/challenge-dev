using System;
using Wappa.Domain.Core.Events;

namespace Wappa.Domain.Events
{
    public class DriverRemovedEvent : Event
    {
        public DriverRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}