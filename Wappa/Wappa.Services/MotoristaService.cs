using System.Threading.Tasks;
using Wappa.Core.Repositories;
using Wappa.Core.Services;
using Wappa.Entities;
using Wappa.Services.Interfaces;

namespace Wappa.Services
{
    public class MotoristaService : ServiceBase<Motorista>, IMotoristaService
    {
        private readonly ICarroService _carroService;
        private readonly IEnderecoService _enderecoService;

        public MotoristaService(IRepositoryBase<Motorista> repository,
                                ICarroService carroService,
                                IEnderecoService enderecoService) : base(repository)
        {
            _carroService    = carroService;
            _enderecoService = enderecoService;
        }

        public override async Task<Motorista> Add(Motorista obj)
        {
            if (obj.Carro != null)
                await _carroService.Add(obj.Carro);

            if (obj.Endereco != null)
                await _enderecoService.Add(obj.Endereco);

            return await base.Add(obj);
        }

    }
}
