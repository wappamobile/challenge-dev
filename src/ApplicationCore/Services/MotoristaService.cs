using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Interfaces.Services;
using PagedList.Core;
using System;
using System.Linq;

namespace ApplicationCore.Services
{
    public class MotoristaService : IMotoristaService
    {
        private readonly IMotoristaRepository _motoristaRepository;

        public MotoristaService(IMotoristaRepository motoristaRepository)
        {
            this._motoristaRepository = motoristaRepository;
        }

        public void Add(Motorista motorista)
        {
            motorista.DataCadastro = DateTime.Now;
            motorista.Ativo = true;
            _motoristaRepository.Add(motorista);
            _motoristaRepository.SaveChanges();
        }

        public void Update(Motorista motorista)
        {
            if (motorista == null)
                throw new MotoristaNaoEncontradoException();

            var motoristaDb = this._motoristaRepository.GetById(motorista.MotoristaId);
            if (motoristaDb == null)
                throw new MotoristaNaoEncontradoException();

            motoristaDb.Nome = motorista.Nome;
            motoristaDb.Sobrenome = motorista.Sobrenome;

            _motoristaRepository.Update(motoristaDb);
            _motoristaRepository.SaveChanges();
        }

        public Motorista Obter(int motoristaId)
        {
            return _motoristaRepository.GetQuery()
                .Where(x => x.MotoristaId == motoristaId)
                .Where(x => x.Ativo)
                .FirstOrDefault();
        }

        public void Delete(int motoristaId)
        {
            var motorista = _motoristaRepository.GetById(motoristaId);
            motorista.Ativo = false;

            _motoristaRepository.SaveChanges();
        }

        public IPagedList<Motorista> Listar(int pageNumber, int pageSize, string sortBy)
        {
            return _motoristaRepository.Listar(pageNumber, pageSize, sortBy);
        }
    }
}
