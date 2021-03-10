using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using Wappa.CoordenadasGeograficas.API.Models;
using Wappa.WebAPI.Core.Controllers;

namespace Wappa.CoordenadasGeograficas.API.Controllers
{
	/// <summary>
	/// API para consultar coordenadas geográficas
	/// </summary>
	[Route("api/geocoordenada")]
	public class CoordenadaGeograficaController : MainController
	{
		/// <summary>
		/// Busca coordenada geográfica de um endereço
		/// </summary>
		/// <param name="endereco">Dados do endereço</param>
		/// <returns>200 ou 400 ou 404</returns>
		[HttpGet]
		[SwaggerResponse((int)HttpStatusCode.OK, Description = "Retorna quando sucesso.")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Retorna quando ocorre alguma falha.")]
		[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Retorna quando ocorre alguma falha.")]
		public async Task<IActionResult> BuscarCoordenadaPorEndereco(Endereco endereco)
		{
			return Ok();
		}
	}
}
