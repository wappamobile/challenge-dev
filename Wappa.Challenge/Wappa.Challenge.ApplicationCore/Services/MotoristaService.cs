using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Wappa.Challenge.ApplicationCore.Entities;
using Wappa.Challenge.ApplicationCore.Interfaces.Repositories;
using Wappa.Challenge.ApplicationCore.Interfaces.Services;

namespace Wappa.Challenge.ApplicationCore.Services
{
    public class MotoristaService : ABaseService<Motorista>, IMotoristaService
    {
        private readonly IMotoristaRepository _motoristaRepository;

        public MotoristaService(IMotoristaRepository motoristaRepository_) : base(motoristaRepository_)
        {
            _motoristaRepository = motoristaRepository_;
        }

        public override Motorista Adicionar(Motorista entity)
        {
            try
            {
                foreach (var item in entity.Endereco)
                {
                    var coordenadas = Util.Google.Coordenadas.ObterPorEndereco($"{item.Logradouro},{item.Numero},{item.Bairro},{item.Cidade}-{item.Estado},{item.Cep}");

                    item.Latitude = coordenadas.Key;
                    item.Longitude = coordenadas.Value;
                }
            }
            catch (Exception ex){ var teste = ex.Message; }

            return base.Adicionar(entity);
        }

        public virtual bool Apagar(long id)
        {
            return _motoristaRepository.Apagar(id);
        }
    }
}
