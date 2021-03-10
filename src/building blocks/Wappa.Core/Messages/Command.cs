using FluentValidation.Results;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Wappa.Core.Messages
{
	public abstract class Command : Message, IRequest<ValidationResult>
	{
		public DateTime TimeStamp { get; private set; }
		[JsonIgnore]
		public ValidationResult ValidationResult { get; set; }

		protected Command()
		{
			TimeStamp = DateTime.Now;
		}

		public virtual bool EhValido()
		{
			throw new NotImplementedException();
		}
	}
}
