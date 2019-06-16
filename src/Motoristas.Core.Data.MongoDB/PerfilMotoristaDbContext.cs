using Motoristas.Core.States;

namespace Motoristas.Core.Data.MongoDB
{
    public class PerfilMotoristaDbContext : MongoDbContext<PerfilMotoristaState>, IPerfilMotoristaDbContext
    {
        public PerfilMotoristaDbContext(string connectionString) : base(connectionString, Collections.PERFIS_MOTORISTA)
        {
        }
    }
}