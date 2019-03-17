using System.Linq;
using WappaMobile.ChallengeDev.Models;

namespace WappaMobile.ChallengeDev.Persistence
{
    class Motoristas : Repository<Motorista>, IMotoristas
    {
        public Motorista BuscarPeloNome(Nome nome)
        {
            return _cache.First(x => x.Nome.Equals(nome));
        }
    }
}