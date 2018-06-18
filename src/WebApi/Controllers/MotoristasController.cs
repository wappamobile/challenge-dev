using ApplicationCore.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System.Linq;
using System.Linq.Dynamic.Core.Exceptions;
using WebApi.ViewModels;
using WebApi.ViewModels.Request;
using WebApi.ViewModels.Response;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MotoristasController : Controller
    {
        private readonly IMotoristaService _motoristaService;
        private readonly IValidator<MotoristaCadastroPostRequest> _postValidator;
        private readonly IValidator<MotoristaCadastroPutRequest> _putValidator;

        public MotoristasController(IMotoristaService motoristaService,
            IValidator<MotoristaCadastroPostRequest> postValidator,
            IValidator<MotoristaCadastroPutRequest> putValidator)
        {
            _motoristaService = motoristaService;
            _postValidator = postValidator;
            _putValidator = putValidator;
        }

        /// <summary>
        /// Retorna uma lista paginada dos motoristas cadastrados
        /// </summary>
        /// <param name="request">Dados de paginação</param>
        /// <response code="422">Erro de validação</response>
        /// <response code="500">Erro inesperado</response>
        [HttpGet]
        public IActionResult Get(MotoristaListagemViewModel request)
        {
            try
            {
                request = request ?? new MotoristaListagemViewModel();
                var dados = this._motoristaService.Listar(request.PageNumber, request.PageSize, request.SortBy);
                request.Itens = new StaticPagedList<CadastroMotoristaViewModel>(dados.Select(x => new CadastroMotoristaViewModel(x)), dados.GetMetaData());
                return Ok(request);
            }
            catch (ParseException)
            {
                return StatusCode(422, new ErroValidacaoResponse("sortBy", "invalid field"));
            }

        }

    }
}
