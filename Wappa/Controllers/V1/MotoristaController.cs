using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wappa.Commom;
using Wappa.Models.Motorista;
using Wappa.Services.V1;

namespace Wappa.Controllers.V1 {
    [ApiExplorerSettings (GroupName = "v1.Motoristas")]
    [Route ("api/v1/[controller]")]
    public class MotoristaController : Controller {

        public MotoristaController (IMotoristaService motoristasService, IGeolocalizacaoService geoService) {
            this.motoristasService = motoristasService;
            this.geoService = geoService;
        }

        private IMotoristaService motoristasService { get; set; }
        private IGeolocalizacaoService geoService { get; set; }

        /// <sumary>
        /// Cadastra um novo motorista na base de motoristas
        /// </sumary>
        /// <response code="200"> Motorista cadastrado com sucesso </response>
        [ProducesResponseType (typeof (void), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] MotoristaModel model) {

            if (ModelState.IsValid) {

                // Fiz essa validação mockada porque montei esta forma de retorno de erros e gostei bastante, vale deixar aqui.
                if (motoristasService.ObterMotorista (model.PrimeiroNome, model.UltimoNome) != null)
                    return Criticas.Motorista.MotoristaJaCadastrado ();

                var dadosGeo = await geoService.ObterGeolocalizacao (model);

                if (dadosGeo.status.Equals ("OK", StringComparison.InvariantCultureIgnoreCase))
                    model.Geolocalizacao = new GeolocalizacaoResult () {
                        Latitude = dadosGeo.results.FirstOrDefault ().geometry.location.lat,
                        Longitude = dadosGeo.results.FirstOrDefault ().geometry.location.lng
                    };

                await motoristasService.GravarMotorista (model);
                return Ok (dadosGeo);
            }

            return BadRequest (ModelState);
        }

        /// <sumary>
        /// Obtem os motoristas cadastrados na base de acordo com ordenação informada
        /// </sumary>
        /// <param name="orderBySobreNome">Ordenação desejada</param>
        /// <response code="200">Motorista cadastrado com sucesso</response>
        [ProducesResponseType (typeof (MotoristaResult), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get ([FromQuery] bool orderBySobreNome) {

            if (ModelState.IsValid) {
                var motoristas = motoristasService.ObterMotoristas (orderBySobreNome);
                return Ok (motoristas);
            }

            return BadRequest (ModelState);
        }

    }
}