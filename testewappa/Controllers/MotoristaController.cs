using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using testewappa.Integration;
using testewappa.Models;
using testewappa.Repository;

namespace testewappa.Controllers
{
    [Route("api/[controller]")]
    /// <summary>
    /// API para cadastro de Motoristas
    /// </summary>
    public class MotoristaController : Controller
    {
        private IWappaRepository _repository { get; set; }
        private IGoogleAddress _googleAddress {get;set;}
        public MotoristaController(IWappaRepository repository, IGoogleAddress google)
        {
            _repository = repository;
            _googleAddress =  google;
        }

        [HttpGet]
        /// <summary>
        /// Lista todos os motoristas ordenados alfabeticamente por Nome( passar "N" ou Sobrenome "S")
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<Motorista> GetAll(string order)
        {
            return _repository.GetAll(order);
        }
        /// <summary>
        /// Retorna Motorista por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}",Name="GetMotorista")]
        public IActionResult GetById(int id)
        {
            var motorista = _repository.Find(id);
            if(motorista == null)
                return NotFound();
            else
                return new ObjectResult(motorista);
        } 
        /// <summary>
        /// Inclui um novo motorista
        /// </summary>
        /// <param name="motorista"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] Motorista motorista)
        {
            if (motorista != null)
                return BadRequest();

            _repository.Add(motorista);

            return CreatedAtRoute("GetMotorista", new Motorista{Id=motorista.Id}, motorista);
        }
        /// <summary>
        /// Atualiza um motorista e grava coordenadas de seu endereço
        /// </summary>
        /// <param name="id"></param>
        /// <param name="motorista"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Motorista motorista)
        {
            if(motorista != null || motorista.Id != id)
                return BadRequest();

            var _motorista = _repository.Find(id);

            if(_motorista == null)
                return NotFound();

            //Campos motorista
            _motorista.Nome = motorista.Nome;
            _motorista.Sobrenome = motorista.Sobrenome;
            if(motorista.Carro != null)
            {
                _motorista.Carro.Marca = motorista.Carro.Marca;
                _motorista.Carro.Modelo = motorista.Carro.Modelo;
                _motorista.Carro.Placa = motorista.Carro.Placa;
            }

            if(motorista.Endereco != null)
            {
                _motorista.Endereco.Descricao = motorista.Endereco.Descricao;

                string[] coordenadas = _googleAddress.GetCoordinates(motorista.Endereco.Descricao);
                _motorista.Endereco.Latitude = coordenadas[0];
                _motorista.Endereco.Longitude = coordenadas[1];
            }
            
            _repository.Update(_motorista);

            return new NoContentResult();
        }
        /// <summary>
        /// Remove um motorista por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var _motorista = _repository.Find(id);

            if(_motorista == null)
                return NotFound();

            _repository.Delete(id);

            return new NoContentResult();
        }
    }
}