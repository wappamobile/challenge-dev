using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Wappa.Domain.Messages;

namespace Wappa.Api.Controllers
{
    /// <summary>
    /// Classe Base para Controllers
    /// </summary>
    public abstract class CustomController : ControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        protected CustomController(IMediator mediator)
        {
            Mediator = mediator;
        }

        /// <summary>
        /// Propriedade para o Mediador
        /// </summary>
        protected IMediator Mediator { get; private set; }

        /// <summary>
        /// Executa o commando atribuído ao handler
        /// </summary>
        /// <param name="command">Commando de requisição</param>
        /// <returns></returns>
        protected async Task<IActionResult> SendAsync(IRequest<Response> command)
        {
            var response = await Mediator.Send(command);

            if (response.HasMessages)
            {
                return BadRequest(response.Messages);
            }

            if (response.Value == null)
            {
                return NoContent();
            }

            return Ok(response.Value);
        }

        /// <summary>
        /// Executa o commando atribuído ao hendler e retorna uma rota
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="command">Commando de requisição</param>
        /// <param name="routeName">Nome da Rota</param>
        /// <param name="routeValue">Valores da rota</param>
        /// <returns></returns>
        protected async Task<IActionResult> SendRouteAsync<T>(IRequest<Response> command,
            string routeName, Func<T, object> routeValue) where T : class
        {
            var response = await Mediator.Send(command);

            if (response.HasMessages)
            {
                return BadRequest(response.Messages);
            }

            if (response.Value == null)
            {
                return NoContent();
            }

            return Ok(CreatedAtRoute(routeName, routeValue?.Invoke((T)response.Value), response.Value));
        }
    }
}