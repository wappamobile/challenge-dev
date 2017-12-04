using System.Threading.Tasks;

namespace Wappa.Service.GeometryService
{
    public interface IGeometryServiceAsync
    {
        Task<Geometry> GetGeometryAsync(string endereco);
        string ObterEnderecoCompleto(string logradouro, string numero, string bairro, string cidade, string estado, string cep);
    }
}
