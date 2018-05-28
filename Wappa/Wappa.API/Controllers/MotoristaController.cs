using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Wappa.API.ViewModel;
using Wappa.Core.APIHandler;
using Wappa.Core.UnitOfWork;
using Wappa.Entities;
using Wappa.Services.Interfaces;

namespace Wappa.API.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel]
    public class MotoristaController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly IMotoristaService _motoristaService;

        public MotoristaController(IUnitOfWork context,
                                   IMapper mapper,
                                   IMotoristaService motoristaService)
        {
            _context      = context;
            _mapper       = mapper;
            _motoristaService = motoristaService;
        }

        // GET api/motorista
        [HttpGet]
        public async Task<IActionResult> Get(bool orderByLastName = false)
        {
            return Json((await _motoristaService.GetAll())
                                                .OrderBy(p => orderByLastName? p.UltimoNome : p.PrimeiroNome));
        }

        // GET api/motorista/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Json((await _motoristaService.GetAll(a => a.Id == id)).FirstOrDefault());
        }

        // POST api/motorista
        [HttpPost]
        public async Task Post([FromBody]Motorista value)
        {
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    await _motoristaService.Add(value);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // PUT api/motorista/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]UpdateMotoristaViewModel value)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<UpdateMotoristaViewModel, Motorista>(value);

                using (_context)
                {
                    await _motoristaService.Add(model);
                    _motoristaService.Update(model);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // DELETE api/motorista/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var motorista = await _motoristaService.GetById(id);

                if (motorista == null)
                    return BadRequest();

                using (_context)
                {
                    _motoristaService.Remove(motorista);
                    await _context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

    }
}
