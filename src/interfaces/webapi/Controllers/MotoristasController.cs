using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WappaMobile.ChallengeDev.GoogleMaps;
using WappaMobile.ChallengeDev.Models;
using WappaMobile.ChallengeDev.Persistence;

namespace WappaMobile.ChallengeDev.Client.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristasController : ControllerBase
    {
        readonly GeoCodeServices _googleMaps = new GeoCodeServices();

        [HttpGet]
        public ActionResult<IEnumerable<Motorista>> Get([FromQuery]string orderby = "")
        {
            var model = Database.Motoristas.Listar();

            if(orderby.ToLower().Equals("nome"))
                return Ok(model.OrderBy(x => x.Nome.Primeiro));

            if(orderby.ToLower().Equals("sobrenome"))
                return Ok(model.OrderBy(x => x.Nome.Ultimo));

            return Ok(model);
        }

        [HttpGet("{id}")]
        public ActionResult<Motorista> Get(Guid id)
        {
            return Ok(Database.Motoristas.PegarPeloId(id));
        }

        [HttpPost]
        public async void Post([FromBody] Motorista value)
        {
            var coord = await _googleMaps.BuscarAsync(value.Endereco);
            value.Endereco.DefinirCoordenadas(coord);
            Database.Motoristas.Incluir(value);
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Motorista value)
        {
            value.Id = id;
            Database.Motoristas.Atualizar(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Database.Motoristas.Excluir(id);
        }
    }
}
