using Microsoft.AspNetCore.Mvc;
using Wappa.Challenge.Domain.Commands.Inputs;
using Wappa.Challenge.Domain.Commands.Outputs;
using Wappa.Challenge.Domain.Handlers;
using Wappa.Challenge.Shared.Commands;

namespace Wappa.Challenge.Api.Controllers
{
    /// <summary>
    /// Driver Controlller
    /// </summary>
    [Produces("application/json")]
    [Route("api/driver")]
    public class DriverController : Controller
    {
        private readonly DriverHandler _handler;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handler"></param>
        public DriverController(DriverHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Inserts a new driver
        /// </summary>
        /// <param name="command">Driver information</param>
        /// <returns></returns>
        [HttpPost]
        public ICommandResult Insert([FromBody]CreateDriverCommand command)
        {
            return _handler.Handle(command);
        }

        /// <summary>
        /// Updates driver data
        /// </summary>
        /// <param name="command">New driver information</param>
        /// <returns></returns>
        [HttpPut]
        public ICommandResult Update([FromBody]UpdateDriverCommand command)
        {
            return _handler.Handle(command);
        }

        /// <summary>
        /// Removes driver from database
        /// </summary>
        /// <param name="command">Driver Id</param>
        /// <returns></returns>
        [HttpDelete]
        public ICommandResult Delete([FromBody]DeleteDriverCommand command)
        {
            return _handler.Handle(command);
        }

        /// <summary>
        /// List drivers
        /// </summary>
        /// <param name="command">Sort definition</param>
        /// <returns></returns>
        [HttpGet("list")]
        public ListDriversCommandResult List([FromQuery]ListDriversCommand command)
        {
            return _handler.Handle(command) as ListDriversCommandResult;
        }
    }
}