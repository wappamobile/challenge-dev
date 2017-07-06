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
    public class ModeloController : Controller
    {
        IModeloRepository modeloRepository;

        public ModeloController(IModeloRepository modeloRepository) {
            this.modeloRepository = modeloRepository;
        }

        // GET api/modelo
        [HttpGet]
        public IEnumerable<Modelo> Get()
        {
            return modeloRepository.get();
        }

        // GET api/modelo/5
        [HttpGet("{id}")]
        public Modelo Get(int id)
        {
            return modeloRepository.get(id);
        }

        // GET api/modelo/pordescricao/[descricao]
        [HttpGet, Route("pordescricao/{descricao}")]
        public IEnumerable<Modelo> Get(String descricao)
        {
            return modeloRepository.get(descricao);
        }
 
        // POST api/modelo
        [HttpPost]
        public int Post([FromBody]Modelo value)
        {
            modeloRepository.add(value);

            return value.ID;
        }

        // PUT api/modelo/5
        [HttpPut]
        public void Put([FromBody]Modelo value)
        {
            modeloRepository.update(value);
        }

        // DELETE api/modelo/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            modeloRepository.delete(id);
        }
    }
}
