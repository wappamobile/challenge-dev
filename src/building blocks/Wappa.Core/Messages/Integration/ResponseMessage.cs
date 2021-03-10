using FluentValidation.Results;

namespace Wappa.Core.Messages.Integration
{
	public class ResponseMessage : Message
	{
		public ValidationResult ValidationResult { get; set; }
		public decimal Longitude { get; set; }
		public decimal Latitude { get; set; }

		public ResponseMessage(ValidationResult validationResult, decimal longitude, decimal latitude)
		{
			ValidationResult = validationResult;
			Longitude = longitude;
			Latitude = latitude;
		}
	}
}
