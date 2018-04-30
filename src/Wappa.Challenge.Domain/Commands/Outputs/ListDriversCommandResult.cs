using System.Collections.Generic;
using Wappa.Challenge.Shared.Commands;

namespace Wappa.Challenge.Domain.Commands.Outputs
{
    public class ListDriversCommandResult : ICommandResult
    {
        public ListDriversCommandResult(bool success, string message, List<DriverResult> drivers)
        {
            Success = success;
            Message = message;
            Drivers = drivers;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public List<DriverResult> Drivers { get; set; }
    }
}