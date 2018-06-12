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
    public class EnderecoService : ABaseService<Endereco>, IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        public EnderecoService(IEnderecoRepository enderecoRepository_) : base(enderecoRepository_)
        {
            _enderecoRepository = enderecoRepository_;
        }

        public override Endereco Adicionar(Endereco entity)
        {
            try
            {
                var googleUrl = $"http://maps.googleapis.com/maps/api/geocode/json?address={entity.Logradouro},{entity.Numero},{entity.Bairro},{entity.Cidade}-{entity.Estado},{entity.Cep}&sensor=true_or_false";
                WebClient wc = new WebClient();
                var json = JsonConvert.DeserializeObject(wc.DownloadString(googleUrl));
                entity.Latitude = ((dynamic)json).results[0].geometry.location.lat;
                entity.Longitude = ((dynamic)json).results[0].geometry.location.lng;
            }
            catch { }

            return base.Adicionar(entity);
        }

        public virtual bool Apagar(long id)
        {
            return _enderecoRepository.Apagar(id);
        }
    }
}
