using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wappa.Domain.Entities;
using Wappa.Domain.Abstract;

namespace Wappa.Controllers
{
    [Route("api/[controller]")]
    public class MarcaController : Controller
    {
        IMarcaRepository marcaRepository;

        public MarcaController(IMarcaRepository marcaRepository) {
            this.marcaRepository = marcaRepository;
        }

        // GET api/marca
        [HttpGet]
        public IEnumerable<Marca> Get()
        {
            return marcaRepository.get();
        }

        // GET api/marca/5
        [HttpGet("{id}")]
        public Marca Get(int id)
        {
            return marcaRepository.get(id);
        }

        // GET api/marca/pordescricao/[descricao]
        [HttpGet, Route("pordescricao/{descricao}")]
        public IEnumerable<Marca> Get(String descricao)
        {
            return marcaRepository.get(descricao);
        }
 
        // POST api/marca
        [HttpPost]
        public int Post([FromBody]Marca value)
        {
            marcaRepository.add(value);

            return value.ID;
        }

        // PUT api/marca/5
        [HttpPut]
        public void Put([FromBody]Marca value)
        {
            marcaRepository.update(value);
        }

        // DELETE api/marca/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            marcaRepository.delete(id);
        }
    }
}
