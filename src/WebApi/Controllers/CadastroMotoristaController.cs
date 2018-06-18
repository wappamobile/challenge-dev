using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels.Request;
using WebApi.ViewModels.Response;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CadastroMotoristaController : Controller
    {
        private readonly IMotoristaService _motoristaService;
        private readonly ICarroService _carroService;
        private readonly IEnderecoService _enderecoService;
        private readonly IValidator<MotoristaCadastroPostRequest> _postValidator;
        private readonly IValidator<MotoristaCadastroPutRequest> _putValidator;

        public CadastroMotoristaController(IMotoristaService motoristaService,
            ICarroService carroService,
            IEnderecoService enderecoService,
            IValidator<MotoristaCadastroPostRequest> postValidator,
            IValidator<MotoristaCadastroPutRequest> putValidator)
        {
            _motoristaService = motoristaService;
            _carroService = carroService;
            _enderecoService = enderecoService;
            _postValidator = postValidator;
            _putValidator = putValidator;
        }

        /// <summary>
        /// Cadastra um novo motorista
        /// </summary>
        /// <param name="request">Dados do motorista</param>
        /// <response code="400">Request inválido</response>
        /// <response code="422">Erro de validação</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPost]
        public IActionResult Post([FromBody]MotoristaCadastroPostRequest request)
        {
            if (request == null)
                return BadRequest();

            var validationResult = _postValidator.Validate(request);
            if (validationResult.IsValid)
            {
                return processarPost(request);
            }
            else
            {
                var erroValidacao = new ErroValidacaoResponse(validationResult);
                return StatusCode(422, erroValidacao);
            }
        }

        /// <summary>
        /// Atualiza o cadastro de um motorista
        /// </summary>
        /// <param name="request">Dados do motorista</param>
        /// <response code="400">Request inválido</response>
        /// <response code="422">Erro de validação</response>
        /// <response code="404">Cadastro não encontrado</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPut]
        public IActionResult Put([FromBody]MotoristaCadastroPutRequest request)
        {
            if (request == null)
                return BadRequest("Motorista não encontrado");

            var validationResult = _putValidator.Validate(request);
            if (validationResult.IsValid)
            {
                return processarPut(request);
            }
            else
            {
                var erroValidacao = new ErroValidacaoResponse(validationResult);
                return StatusCode(422, erroValidacao);
            }
        }

        /// <summary>
        /// Remove o cadastro de um motorista
        /// </summary>
        /// <param name="id">Id do motorista</param>
        /// <response code="404">Motorista não encontrado</response>
        /// <response code="500">Erro inesperado</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var motorista = _motoristaService.Obter(id);

            if(motorista == null)
                return StatusCode(404, "Motorista não encontrado");

            _motoristaService.Delete(id);

            return Ok("deleted");
        }

        private IActionResult processarPost(MotoristaCadastroPostRequest viewModel)
        {
            var carro = viewModel.ToCarroModel();
            _carroService.Add(carro);

            var endereco = viewModel.ToEnderecoModel();
            _enderecoService.Add(endereco);

            var motorista = viewModel.ToMotoristaModel();
            motorista.CarroId = carro.CarroId;
            motorista.EnderecoId = endereco.EnderecoId;

            _motoristaService.Add(motorista);

            return Ok("success");
        }

        private IActionResult processarPut(MotoristaCadastroPutRequest viewModel)
        {
            try
            {
                var motoristaDb = _motoristaService.Obter(viewModel.MotoristaId);
                if (motoristaDb == null || !motoristaDb.Ativo)
                    return NotFound();

                var carro = viewModel.ToCarroModel(motoristaDb.CarroId);
                _carroService.Update(carro);

                var endereco = viewModel.ToEnderecoModel(motoristaDb.EnderecoId);
                _enderecoService.Update(endereco);

                var motorista = viewModel.ToMotoristaModel();

                _motoristaService.Update(motorista);

                return Ok("success");
            }
            catch(MotoristaNaoEncontradoException)
            {
                return NotFound("Motorista não encontrado");
            }
            catch(CarroNaoEncontradoException)
            {
                return NotFound("Carro não encontrado");
            }
            catch (EnderecoNaoEncontradoException)
            {
                return NotFound("Endereco não encontrado");
            }

        }

    }
}
