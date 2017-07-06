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
    public class EnderecoController : Controller
    {
        IEnderecoRepository enderecoRepository;

        public EnderecoController(IEnderecoRepository enderecoRepository) {
            this.enderecoRepository = enderecoRepository;
        }

        // GET api/endereco
        [HttpGet]
        public IEnumerable<Endereco> Get()
        {
            return enderecoRepository.get();
        }

        // GET api/endereco/5
        [HttpGet("{id}")]
        public Endereco Get(int id)
        {
            return enderecoRepository.get(id);
        }

        // POST api/endereco
        [HttpPost]
        public int Post([FromBody]Endereco value)
        {
            enderecoRepository.add(value);

            return value.ID;
        }

        // PUT api/endereco/5
        [HttpPut]
        public void Put([FromBody]Endereco value)
        {
            enderecoRepository.update(value);
        }

        // DELETE api/endereco/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            enderecoRepository.delete(id);
        }
    }
}
