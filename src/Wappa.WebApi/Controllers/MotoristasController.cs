using Microsoft.AspNetCore.Mvc;
using Wappa.Domain.UnitOfWork;
using Wappa.WebApi.ViewModels.Response;
using Wappa.WebApi.ViewModels.Request;
using FluentValidation;
using System.Net;
using Wappa.Service.GeometryService;
using Wappa.Domain.Entities;
using System.Threading.Tasks;

namespace Wappa.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Motoristas")]
    public class MotoristasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PostMotoristasRequest> _validator;
        private readonly IGeometryServiceAsync _geometryServiceAsync;

        public MotoristasController(IUnitOfWork unitOfWork,
                                    IValidator<PostMotoristasRequest> validator,
                                    IGeometryServiceAsync geometryServiceAsync)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _geometryServiceAsync = geometryServiceAsync;
        }

        // GET api/motoristas
        [HttpGet]
        public GetMotoristasResponse Get(GetMotoristasRequest request)
        {
            return new GetMotoristasResponse(_unitOfWork.GetMotoristaRepository().Listar(request.ObterOrdenacao()));
        }

        // POST api/motoristas
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PostMotoristasRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (validationResult.IsValid)
            {
                var carro = _unitOfWork.GetCarroRepository().Obter(request.CarroId);
                var cidade = _unitOfWork.GetCidadeRepository().Obter(request.CidadeId);

                var endereco = _geometryServiceAsync.ObterEnderecoCompleto(request.Logradouro,
                                                            request.Numero,
                                                            request.Bairro,
                                                            cidade.Nome,
                                                            cidade.Estado.Descricao,
                                                            request.Cep);

                var geometry = await _geometryServiceAsync.GetGeometryAsync(endereco);

                if (!geometry.IsValid)
                    return StatusCode((int)HttpStatusCode.MethodNotAllowed);

                var motorista = _unitOfWork.GetMotoristaRepository().Adicionar(new Motorista
                {
                    Nome = request.Nome,
                    Sobrenome = request.Sobrenome,
                    Carro = carro,
                    Logradouro = request.Logradouro,
                    Numero = request.Numero,
                    Bairro = request.Bairro,
                    Cep = request.Cep,
                    Cidade = cidade,
                    Latitude = geometry.Latitude,
                    Longitude = geometry.Longitude
                });
                return StatusCode((int)HttpStatusCode.NoContent);
            }
            else
            {
                var erroValidacao = new ErroValidacaoResponse(validationResult);

                return StatusCode((int)HttpStatusCode.MethodNotAllowed, erroValidacao);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PostMotoristasRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (validationResult.IsValid)
            {
                var carro = _unitOfWork.GetCarroRepository().Obter(request.CarroId);
                var cidade = _unitOfWork.GetCidadeRepository().Obter(request.CidadeId);

                var motorista = _unitOfWork.GetMotoristaRepository().Obter(id);
                motorista.Nome = request.Nome;
                motorista.Sobrenome = request.Sobrenome;
                motorista.Carro = carro;
                motorista.Logradouro = request.Logradouro;
                motorista.Numero = request.Numero;
                motorista.Bairro = request.Bairro;
                motorista.Cep = request.Cep;
                motorista.Cidade = cidade;

                var endereco = _geometryServiceAsync.ObterEnderecoCompleto(request.Logradouro,
                                                            request.Numero,
                                                            request.Bairro,
                                                            cidade.Nome,
                                                            cidade.Estado.Descricao,
                                                            request.Cep);

                var geometry = await _geometryServiceAsync.GetGeometryAsync(endereco);

                if (!geometry.IsValid)
                    return StatusCode((int)HttpStatusCode.MethodNotAllowed);

                motorista.Latitude = geometry.Latitude;
                motorista.Longitude = geometry.Longitude;

                motorista = _unitOfWork.GetMotoristaRepository().Atualizar(motorista);
                return StatusCode((int)HttpStatusCode.NoContent);
            }
            else
            {
                var erroValidacao = new ErroValidacaoResponse(validationResult);

                return StatusCode((int)HttpStatusCode.MethodNotAllowed, erroValidacao);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _unitOfWork.GetMotoristaRepository().Excluir(id);

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        //// GET api/cidades
        //[HttpGet]
        //public async Task<IEnumerable<Geometry>> Get()
        //{
        //    var motoristas = _unitOfWork.GetMotoristaRepository().Listar();
        //    var carro = _unitOfWork.GetCarroRepository().Obter(1);
        //    var cidade = _unitOfWork.GetCidadeRepository().Obter(1);

        //    var motorista = motoristas.First();

        //    var geometry = await _geometryServiceAsync.GetGeometryAsync(motorista.ObterEnderecoCompleto());

        //    return new[] { geometry };
        //}


    }
}