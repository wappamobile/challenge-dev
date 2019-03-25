using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotoristaBusiness;

namespace Motorista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristaController : ControllerBase
    {
        private readonly IMotoristaService _motoristaService;

        public ActionResult<List<MotoristaEntity.Motorista>> Get()
        {
            return _motoristaService.GetMotorista();
        }

        public MotoristaController(IMotoristaService motoristaService)
        {
            _motoristaService = motoristaService;
        }
        // GET api/motorista/5
        [HttpGet("{id}")]
        public ActionResult<MotoristaEntity.Motorista> Get(int id)
        {
            var motorista = _motoristaService.GetMotorista(id);
            if (motorista == null)
            {
                return NotFound();
            }
            else
                return motorista;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] MotoristaEntity.Motorista motorista)
        {
            _motoristaService.AddMotorista(motorista);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MotoristaEntity.Motorista motorista)
        {
            motorista.Id = id;
            _motoristaService.UpdateMotorista(motorista);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _motoristaService.DeleteMotorista(id);
        }

    }
}