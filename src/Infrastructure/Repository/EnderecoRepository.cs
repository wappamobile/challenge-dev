using ApplicationCore.Entity;
using ApplicationCore.Interfaces.Repository;
using Infra.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(Context dbContext) : base(dbContext)
        {

        }
    }
}
