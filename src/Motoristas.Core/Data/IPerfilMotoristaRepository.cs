using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motoristas.Core.Data
{
    public interface IPerfilMotoristaRepository
    {
        Task Save(PerfilMotorista perfilMotorista);
        Task<PerfilMotorista> Load(string id);
        Task Delete(string id);
        Task<List<PerfilMotorista>> ListAll(IFiltroPerfilMotorista filtro);

    }
}