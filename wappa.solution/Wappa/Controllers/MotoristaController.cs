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
    public class MotoristaController : Controller
    {
        IMotoristaRepository motoristaRepository;

        public MotoristaController(IMotoristaRepository motoristaRepository) {
            this.motoristaRepository = motoristaRepository;
        }

        // GET api/motorista
        [HttpGet]
        public IEnumerable<Motorista> Get()
        {
            return motoristaRepository.get();
        }

        // GET api/motorista/5
        [HttpGet("{id}")]
        public Motorista Get(int id)
        {
            return motoristaRepository.get(id);
        }


        // POST api/motorista
        [HttpPost]
        public int Post([FromBody]Motorista value)
        {
            motoristaRepository.add(value);

            return value.ID;
        }

        // PUT api/motorista/5
        [HttpPut]
        public void Put([FromBody]Motorista value)
        {
            motoristaRepository.update(value);
        }

        // DELETE api/motorista/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            motoristaRepository.delete(id);
        }
    }
}
