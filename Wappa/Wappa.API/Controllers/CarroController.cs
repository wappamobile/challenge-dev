using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Core.APIHandler;
using Wappa.Core.UnitOfWork;
using Wappa.Entities;
using Wappa.Services.Interfaces;

namespace Wappa.API.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel]
    public class CarroController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly ICarroService _carroService;

        public CarroController(IUnitOfWork context,
                               ICarroService carroService)
        {
            _context = context;
            _carroService = carroService;
        }

        // GET api/carro
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cursos = (await _carroService.GetAll()).ToList();
            return Json(cursos);
        }

        // GET api/carro/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Json(await _carroService.GetAll(a => a.Id == id));
        }

        // POST api/carro
        [HttpPost]
        public async Task Post([FromBody]Carro value)
        {
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    await _carroService.Add(value);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // PUT api/carro/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Carro value)
        {
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    await _carroService.Add(value);
                    _carroService.Update(value);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // DELETE api/carro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var carro = await _carroService.GetById(id);

                if (carro == null)
                    return BadRequest();

                using (_context)
                {
                    _carroService.Remove(carro);
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
