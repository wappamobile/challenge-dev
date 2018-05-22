using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wappa.Core.Models;
using Wappa.Service.Services;

namespace Wappa.Api.Controllers
{
    [Route("api/[controller]")]
    public class MotoristaController : Controller
    {
        private readonly IMotoristaService _service;

        private readonly ILogger _logger;
        
        public MotoristaController(IMotoristaService service, ILogger<MotoristaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string sessionId = Guid.NewGuid().ToString();

            try
            {
                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.Get"
                    , sessionId);
                         
                var result = await _service.GetAll();

                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.Get {1}"
                    , sessionId, JsonConvert.SerializeObject(result));

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError("[{0}][ERROR]Wappa.Api.Controllers.MotoristaController.Get {1}"
                    , sessionId, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            string sessionId = Guid.NewGuid().ToString();

            try
            {
                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.Get({1})"
                    , sessionId, id);

                var result = await _service.Get(id);

                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.Get({1}) {2}"
                    , sessionId, id, JsonConvert.SerializeObject(result));

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError("[{0}][ERROR]Wappa.Api.Controllers.MotoristaController.Get({1}) {2}"
                    , sessionId, id, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("{orderBy}")]
        public async Task<IActionResult> GetAll(string ordem)
        {
            string sessionId = Guid.NewGuid().ToString();

            try
            {
                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.GetAll({1})"
                    , sessionId, ordem);

                var result = await _service.GetAll(ordem);

                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.GetAll({1}) {2}"
                    , sessionId, ordem, JsonConvert.SerializeObject(result));

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError("[{0}][ERROR]Wappa.Api.Controllers.MotoristaController.GetAll({1}) {2}"
                    , sessionId, ordem, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Motorista motorista)
        {
            string sessionId = Guid.NewGuid().ToString();

            try
            {
                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.Post {1}"
                    , sessionId, JsonConvert.SerializeObject(motorista));

                var result = await _service.Save(motorista);

                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.Post {1}"
                    , sessionId, JsonConvert.SerializeObject(result));

                return Created("/motorista", result);
            }
            catch(Exception ex)
            {
                _logger.LogError("[{0}][ERROR]Wappa.Api.Controllers.MotoristaController.Post {1}"
                    , sessionId, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Motorista motorista)
        {
            string sessionId = Guid.NewGuid().ToString();

            try
            {
                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.Put({1}) {2}"
                    , sessionId, id, JsonConvert.SerializeObject(motorista));

                motorista.Id = id;
                var result = await _service.Update(motorista);

                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.Put({1}) {2}"
                    , sessionId, id, JsonConvert.SerializeObject(result));

                return Created("/motorista", result);
            }
            catch(Exception ex)
            {
                _logger.LogError("[{0}][ERROR]Wappa.Api.Controllers.MotoristaController.Put({1}) {2}"
                    , sessionId, id, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string sessionId = Guid.NewGuid().ToString();

            try
            {
                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.Delete({1})"
                    , sessionId, id);

                var result = await _service.Delete(id);

                _logger.LogInformation("[{0}][INFO]Wappa.Api.Controllers.MotoristaController.Delete({1}) {2}"
                    , sessionId, id, JsonConvert.SerializeObject(result));

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError("[{0}][ERROR]Wappa.Api.Controllers.MotoristaController.Delete({1}) {2}"
                    , sessionId, id, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
