using FluentValidation.Results;

namespace Wappa.Core.Messages.Integration
{
	public class ResponseMessage : Message
	{
		public ValidationResult ValidationResult { get; set; }

		public ResponseMessage(ValidationResult validationResult)
		{
			ValidationResult = validationResult;
		}
	}
}
