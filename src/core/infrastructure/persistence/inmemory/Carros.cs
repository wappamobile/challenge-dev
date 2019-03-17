using System.Linq;
using WappaMobile.ChallengeDev.Models;

namespace WappaMobile.ChallengeDev.Persistence
{
    internal class Carros : Repository<Carro>, ICarros
    {
        public Carro BuscarPelaPlaca(Placa placa)
        {
            return _cache.First(x => x.Placa.Equals(placa));
        }
    }
}