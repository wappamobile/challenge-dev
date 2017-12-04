using Microsoft.AspNetCore.Mvc;
using Wappa.Domain.UnitOfWork;
using Wappa.WebApi.ViewModels.Response;

namespace Wappa.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Cidades")]
    public class CidadesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CidadesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public GetCidadesResponse Get()
        {
            return new GetCidadesResponse(_unitOfWork.GetCidadeRepository().ListarTodos());
        }
    }
}