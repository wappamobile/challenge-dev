using Wappa.Challenge.Shared.Commands;

namespace Wappa.Challenge.Domain.Commands.Outputs
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}