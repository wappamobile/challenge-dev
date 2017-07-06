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
    public class CarroController : Controller
    {
        ICarroRepository carroRepository;

        public CarroController(ICarroRepository carroRepository) {
            this.carroRepository = carroRepository;
        }

        // GET api/carro
        [HttpGet]
        public IEnumerable<Carro> Get()
        {
            return carroRepository.get();
        }

        // GET api/carro/5
        [HttpGet("{id}")]
        public Carro Get(int id)
        {
            return carroRepository.get(id);
        }

        // GET api/carro/porplaca/[placa]
        [HttpGet, Route("porplaca/{placa}")]
        public Carro Get(String placa)
        {
            return carroRepository.get(placa);
        }
 
        // POST api/carro
        [HttpPost]
        public int Post([FromBody]Carro value)
        {
            carroRepository.add(value);

            return value.ID;
        }

        // PUT api/carro/5
        [HttpPut]
        public void Put([FromBody]Carro value)
        {
            carroRepository.update(value);
        }

        // DELETE api/carro/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            carroRepository.delete(id);
        }
    }
}
