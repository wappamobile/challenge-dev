using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wappa.Domain.UnitOfWork;
using Wappa.Service.GeometryService;
using Wappa.Domain.Entities;
using Wappa.WebApi.ViewModels.Response;

namespace Wappa.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Carros")]
    public class CarrosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarrosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public GetCarrosResponse Get()
        {
            return new GetCarrosResponse(_unitOfWork.GetCarroRepository().ListarTodos());
        }
    }
}