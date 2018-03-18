using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wappa.Application;
using Wappa.Models;

namespace Wappa.ChallengeDev.Controllers.Api
{
    /// <summary>
    /// Web Api de Taxistas da WAPPA
    /// </summary>
    [Produces("application/json")]
    [Route("api/Taxista")]
    public class TaxistaController : Controller
    {
        private ITaxistaFacade taxistaFacade;
        public TaxistaController(ITaxistaFacade taxistaFacade)
        {
            this.taxistaFacade = taxistaFacade;
        }
        /// <summary>
        /// Remove o taxista pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "Delete")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult Delete(int id)
        {
            var affectedRows = taxistaFacade.Delete(id);
            return new ObjectResult(new { Success = affectedRows > 0 });
        }
        /// <summary>
        /// Busca o taxista pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(typeof(Taxista), 200)]
        public IActionResult GetById(int id)
        {
            var taxista = taxistaFacade.Find(id);
            return new ObjectResult(taxista);
        }
        /// <summary>
        /// Retorna todos os taxistas cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(List<Taxista>), 200)]
        public IActionResult GetAll()
        {
            var taxistas = taxistaFacade.List();
            return new ObjectResult(taxistas);
        }
        /// <summary>
        /// Insere ou atualiza um taxista
        /// </summary>
        /// <param name="taxista"></param>
        /// <returns></returns>
        [HttpPost(Name = "Save")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> Save(Taxista taxista)
        {
            var taxistas = await taxistaFacade.Save(taxista);
            return new ObjectResult(taxistas);
        }
    }
}