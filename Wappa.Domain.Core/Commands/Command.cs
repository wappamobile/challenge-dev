using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Domain.Core.Events;

namespace Wappa.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
