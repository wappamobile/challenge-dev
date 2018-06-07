using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebFC.Wappa.Teste.APIWeb.Helpers;
using WebFC.Wappa.Teste.APIWeb.Models;
using WebFC.Wappa.Teste.Base.Core.Interface;
using WebFC.Wappa.Teste.Base.Core.Models;

namespace WebFC.Wappa.Teste.APIWeb.Controllers
{
    [RoutePrefix("api/motoristas")]
    public class MotoristasController : ApiController
    {

        private IMotoristasServices _MotoristasServices;
        private ICarrosServices _CarrosServices; 

        public MotoristasController()
        {


        }

        public MotoristasController(IMotoristasServices MotoristasServices, ICarrosServices CarrosServices)
        {

            _MotoristasServices = MotoristasServices;
            _CarrosServices = CarrosServices; 

        }

        [Route("listar")]
        // GET: api/Motoristas
        public object GetAll()
        {
            IEnumerable<Motoristas> motoristas = _MotoristasServices.GetAllFilter(x => true, "Nome asc, SegundoNome asc");
            return motoristas; 
        }

        // GET: api/Motoristas/5
        public object Get(Guid id)
        {
            IEnumerable<Motoristas> motoristas = _MotoristasServices.GetAllFilter(x => x.IdMotorista == id , "Nome");
            return motoristas;
        }

        // POST: api/Motoristas
        [HttpPost]
        [Route("Add")]
        public object Add(Motoristas motoristaNovo)
        {

            try {  

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            GoogleGeoCodeResponse geoCodeResponse = GoogleGeoCodeHelpers.GetCoordenadas(motoristaNovo.Endereco); 
            string Lat = geoCodeResponse.results.FirstOrDefault().geometry.location.lat;
            string Long = geoCodeResponse.results.FirstOrDefault().geometry.location.lng;
            motoristaNovo.Lat = Lat;
            motoristaNovo.Long = Long;
            motoristaNovo.IdMotorista = Guid.NewGuid();
            motoristaNovo.Carro.FirstOrDefault().IdCarro = Guid.NewGuid(); 

            Motoristas novoMotorista = _MotoristasServices.Add(motoristaNovo);
            return novoMotorista;

            }
            catch (Exception Ex)
            {
                object r = new
                {
                    Ex.Message

                };
                return r; 

            }
        }
        
        [HttpPost]
        [Route("editar")]
        public object Edit(Motoristas editarMotorista)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Motoristas Motorista = _MotoristasServices.GetAllFilter(x => x.IdMotorista == editarMotorista.IdMotorista, "IdMotorista").First();

                if (Motorista == null)
                {
                    throw new Exception("Motorista não localizado."); 

                }
          
                if(Motorista.Endereco != editarMotorista.Endereco)
                {
                    GoogleGeoCodeResponse geoCodeResponse = GoogleGeoCodeHelpers.GetCoordenadas(editarMotorista.Endereco);

                    string Lat = geoCodeResponse.results.FirstOrDefault().geometry.location.lat;
                    string Long = geoCodeResponse.results.FirstOrDefault().geometry.location.lng;
                    editarMotorista.Lat = Lat;
                    editarMotorista.Long = Long;
                }


                Motoristas motoristaEditado = _MotoristasServices.Update(editarMotorista);

                return motoristaEditado; 
                
            }
            catch (Exception Ex)
            {

                object r = new
                {

                    Ex.Message
                };

                return r; 
            }
        }
       // DELETE: api/Motoristas/5
        public object Delete(Guid id)
        {

            Motoristas motoristas = _MotoristasServices.GetAllFilter(x => x.IdMotorista == id, "Nome").FirstOrDefault();
            _MotoristasServices.Remove(motoristas);

            return motoristas; 
        }
    }
}
