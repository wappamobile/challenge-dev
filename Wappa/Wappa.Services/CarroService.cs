using Wappa.Core.Repositories;
using Wappa.Core.Services;
using Wappa.Entities;
using Wappa.Services.Interfaces;

namespace Wappa.Services
{
    public class CarroService : ServiceBase<Carro>, ICarroService
    {
        public CarroService(IRepositoryBase<Carro> repository) : base(repository)
        {
        }
    }
}
