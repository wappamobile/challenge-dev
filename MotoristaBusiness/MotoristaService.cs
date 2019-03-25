using System;
using System.Linq;
using System.Collections.Generic;
using MotoristaDB;
using MotoristaEntity;
using Microsoft.EntityFrameworkCore;

namespace MotoristaBusiness
{
    public class MotoristaService : IMotoristaService
    {
        private MotoristaContext _motoristaContext;
        private IGeoCodeService _geoCodeService;
        public MotoristaService(MotoristaContext motoristaContext, IGeoCodeService geoCodeService)
        {
            _motoristaContext = motoristaContext;
            _geoCodeService = geoCodeService;
        }

        public Motorista AddMotorista(Motorista motorista)
        {
            //Obtém Longitude/Latitude
            motorista = GetLocationAsync(motorista).Result;

            _motoristaContext.Motoristas.Add(motorista);
            _motoristaContext.SaveChanges();
            return motorista;
        }
        public int DeleteMotorista(int motoristaId)
        {
            var motorista = _motoristaContext.Motoristas.Find(motoristaId);
            if (motorista.Id > 0)
            {
                _motoristaContext.Motoristas.Remove(motorista);
                return _motoristaContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public List<Motorista> GetMotorista()
        {
            var motoristas = _motoristaContext.Motoristas.Include(m => m.Enderecos).Include(m => m.Carros).ToList();
            return motoristas;
        }

        public Motorista GetMotorista(int motoristaId)
        {
            return _motoristaContext.Motoristas.Include(m => m.Enderecos).Include(m => m.Carros).FirstOrDefault(f => f.Id == motoristaId);
        }
        public int UpdateMotorista(Motorista motorista)
        {
            motorista = GetLocationAsync(motorista).Result;
            _motoristaContext.Attach<Motorista>(motorista);
            return _motoristaContext.SaveChanges();
        }

        private async System.Threading.Tasks.Task<Motorista> GetLocationAsync(Motorista motorista)
        {
            //Obtém Longitude/Latitude
            foreach (Endereco endereco in motorista.Enderecos)
            {
                string enderecoCompleto = endereco.Logradouro + ", "
                    + endereco.Numero + ", "
                    + endereco.Bairro + ", "
                    + endereco.Cidade + ", "
                    + endereco.Estado;
                GeoCode geoCode = await _geoCodeService.GetGeoCode(enderecoCompleto);

                if (geoCode != null && geoCode.status.Equals("OK"))
                {
                    endereco.Latitude = geoCode.results[0].geometry.location.lat.ToString();
                    endereco.Longitude = geoCode.results[0].geometry.location.lng.ToString();
                    endereco.GooglePlaceId = geoCode.results[0].place_id;
                }
            }

            return motorista;
        }
    }
}
