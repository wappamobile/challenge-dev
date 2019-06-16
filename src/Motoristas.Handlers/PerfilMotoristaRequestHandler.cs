using MediatR;  
using Motoristas.Core;
using Motoristas.Core.Data;
using Motoristas.Core.Services;
using Motoristas.Handlers.Commands;
using Motoristas.Handlers.Mappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Motoristas.Handlers
{
    public class PerfilMotoristaRequestHandler : IRequestHandler<RemoverPerfilMotorista, RemoverPerfilMotoristaResponse>,
                                        IRequestHandler<RegistrarPerfilMotorista, RegistrarPerfilMotoristaResponse>,
                                        IRequestHandler<ObterPerfilMotorista, ObterPerfilMotoristaResponse>,
                                        IRequestHandler<PerfilMotoristaQuery, PerfilMotoristaQueryResponse>
    {
        private readonly IIdentityGenerator<string> _identityGenerator;

        private readonly IPerfilMotoristaRepository _repository;

        private readonly IGeolocationService _geolocationService;

        public PerfilMotoristaRequestHandler(IIdentityGenerator<string> identityGenerator, 
                                                IPerfilMotoristaRepository repository,
                                                IGeolocationService geolocationService)
        {
            _identityGenerator = identityGenerator;
            _repository = repository;
            _geolocationService = geolocationService;
        }

        public async Task<RegistrarPerfilMotoristaResponse> Handle(RegistrarPerfilMotorista request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var id = _identityGenerator.Create();
            var perfil = new PerfilMotorista(id);
            perfil.DefinirMotorista(request.Motorista.ToDomain());
            perfil.DefinirCoordenadasEnderco(_geolocationService.ObterCoordenadas(request.Motorista.Endereco.Descricao));
            await _repository.Save(perfil);
            var result = new RegistrarPerfilMotoristaResponse { PerfilId = id };
            return result;
        }

        public async Task<ObterPerfilMotoristaResponse> Handle(ObterPerfilMotorista request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var perfil = await _repository.Load(request.PerfilId);
            var result = new ObterPerfilMotoristaResponse { Perfil = perfil };
            return result;
        }

        public async Task<RemoverPerfilMotoristaResponse> Handle(RemoverPerfilMotorista request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            await _repository.Delete(request.PerfilId);
            var result = new RemoverPerfilMotoristaResponse();
            return result;
        }

        public async Task<PerfilMotoristaQueryResponse> Handle(PerfilMotoristaQuery request, CancellationToken cancellationToken)
        {
            var coll = _repository.ListAll(request).Result;
            return new PerfilMotoristaQueryResponse { Lista = coll };
        }
    }
}
