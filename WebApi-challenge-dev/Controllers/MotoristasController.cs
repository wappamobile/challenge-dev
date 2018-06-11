using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using WebApi_challengedev.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi_challengedev.Controllers
{
    [Route("api/[controller]")]
    public class MotoristasController : Controller
    {
        private readonly MotoristasContext _context;

        public MotoristasController(MotoristasContext context)
        {
            _context = context;
        }

        // GET: api/<controller> buscando todos os motoristas
        [HttpGet]
        public IActionResult Get()
        {
            List<Motoristas> motors = new List<Motoristas>();
            motors = _context.ObterListaMotorista();

            if (motors.Count > 0)
            {
                var res = motors.ToJson();
                return new ObjectResult(res);
            }
            else
            {
                return NotFound();
            }

        }

        // GET api/<controller>/ buscando 1 resultado
        [HttpGet("{nome}")]
        public IActionResult Get(string nome, string sobrenome)
        {
            Motoristas moto = new Motoristas();
            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(sobrenome))
            {
                moto = _context.ObterMotorista(nome, sobrenome);
            }
            else if (!string.IsNullOrEmpty(nome))
            {
                moto = _context.ObterMotorista(nome);
            }
            else
            {
                return NotFound();
            }

            var res = moto.ToJson();
            return new ObjectResult(res);

        }

        // POST api/<controller>/UP - update / IN - insert
        [HttpPost("{value}")]
        public IActionResult Post(string value, string model)
        {
            string result = string.Empty;
            string exception = "Não foi possivel realizar a operação";


            if (!string.IsNullOrEmpty(model))
            {

                Motoristas moto = JsonConvert.DeserializeObject<Motoristas>(model);

                switch (value.ToUpper().ToString())
                {
                    case "UP":
                        result = _context.UpadateMotorista(moto);
                        break;

                    case "IN":
                        result = _context.CriarMotorista(moto);
                        break;

                    default:
                        break;
                }

                return (!result.Contains("ERRO")) ? CreatedAtAction("Get", moto.Nome, moto._id) : new ObjectResult(exception);
            }
            else
            {
                return new ObjectResult(exception);
            }

        }

        // DELETE api/<controller>/ Delete do objeto
        [HttpDelete("{value}")]
        public IActionResult Delete(string value, string _id=null,string nome=null, string sobrenome =null)
        {
            string result = string.Empty;
            string excption = "Não foi possivel realizar a operação";
           

            switch (value.ToUpper().ToString())
            {
                

                case "NOME":

                    if (nome != null)
                    {
                        result = _context.DeletarMotorista(nome);
                    }
                    else
                    {
                        return new ObjectResult(excption);
                    }

                    break;

                case "SOBRE":

                    if (nome != null && sobrenome != null)
                    {
                        result = _context.DeletarMotorista(nome, sobrenome);
                    }
                    else
                    {
                        return new ObjectResult(excption);
                    }

                    break;

                default:
                    break;
            }

            
            
            if (!result.Contains("ERRO"))
            {
                string rr = "DELETE de objeto id: " + _id + " executado: " + result;
                return new ObjectResult(rr);
            }
            else
            {
                return new ObjectResult(excption);
            }
    
        }

        // PUT api/<controller>/ ainda não usado
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

    }
}