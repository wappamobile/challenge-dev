using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Motoristas.Core.States;

namespace Motoristas.Core.Data.MongoDB
{
    public class PerfilMotoristaRepository : IPerfilMotoristaRepository
    {
        private readonly IPerfilMotoristaDbContext _dbContext;
        private readonly IMediator _mediator;

        public PerfilMotoristaRepository(IPerfilMotoristaDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Save(PerfilMotorista PerfilMotorista)
        {
            var state = PerfilMotorista.GetState() as PerfilMotoristaState;
            await _dbContext.Save(state);
        }

        public Task<PerfilMotorista> Load(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException(nameof(id));
            var state = _dbContext.Load(id);
            if (state == null) throw new EntityNotFoundException(typeof(PerfilMotorista).Name, id);
            return Task.FromResult(new PerfilMotorista(state));
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException(nameof(id));
            var state = _dbContext.Load(id);
            if (state == null) throw new EntityNotFoundException(typeof(PerfilMotorista).Name, id);
            _dbContext.Delete(id);
        }

        public async Task<List<PerfilMotorista>> ListAll(IFiltroPerfilMotorista filtro)
        {
            var result = _dbContext.Find(filtro.Sort);
            if (result == null) throw new InvalidOperationException();
            var coll = new List<PerfilMotorista>();
            result.ForEach(x => coll.Add(new PerfilMotorista(x)));
            return Task.FromResult(coll).Result;

        }
    }
}