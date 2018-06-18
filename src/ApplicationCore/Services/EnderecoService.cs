using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Interfaces.Services;
using System;

namespace ApplicationCore.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IGeocodeService _geocodeService;

        public EnderecoService(IEnderecoRepository enderecoRepository,
            IGeocodeService geocodeService)
        {
            this._enderecoRepository = enderecoRepository;
            this._geocodeService = geocodeService;
        }

        public void Add(Endereco endereco)
        {
            endereco.DataCadastro = DateTime.Now;
            endereco.Ativo = true;
            _enderecoRepository.Add(endereco);
            _enderecoRepository.SaveChanges();

            SetGeometry(endereco.EnderecoId);

        }

        public void Update(Endereco endereco)
        {
            if (endereco == null)
                throw new EnderecoNaoEncontradoException();

            var enderecoDb = this._enderecoRepository.GetById(endereco.EnderecoId);
            if (enderecoDb == null)
                throw new EnderecoNaoEncontradoException();

            enderecoDb.CEP = endereco.CEP;
            enderecoDb.Cidade = endereco.Cidade;
            enderecoDb.Complemento = endereco.Complemento;
            enderecoDb.Logradouro = endereco.Logradouro;
            enderecoDb.Numero = endereco.Numero;
            enderecoDb.UF = endereco.UF;

            _enderecoRepository.Update(enderecoDb);
            _enderecoRepository.SaveChanges();

            SetGeometry(enderecoDb.EnderecoId);
        }

        private void SetGeometry(int enderecoId)
        {
            _geocodeService.SetGeometryAsync(enderecoId);
        }

    }
}
