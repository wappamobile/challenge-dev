using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wappa.Core.APIHandler;
using Wappa.Core.UnitOfWork;
using Wappa.Entities;
using Wappa.Services.Interfaces;

namespace Wappa.API.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel]
    public class EnderecoController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IUnitOfWork context,
                                  IEnderecoService enderecoService)
        {
            _context = context;
            _enderecoService = enderecoService;
        }

        // GET api/endereco
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _enderecoService.GetAll());
        }

        // GET api/endereco/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Json(await _enderecoService.GetAll(a => a.Id == id));
        }

        // POST api/endereco
        [HttpPost]
        public async Task Post([FromBody]Endereco value)
        {
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    await _enderecoService.Add(value);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // PUT api/endereco/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Endereco value)
        {
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    await _enderecoService.Add(value);
                    _enderecoService.Update(value);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // DELETE api/endereco/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var endereco = await _enderecoService.GetById(id);

                if (endereco == null)
                    return BadRequest();

                using (_context)
                {
                    _enderecoService.Remove(endereco);
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
