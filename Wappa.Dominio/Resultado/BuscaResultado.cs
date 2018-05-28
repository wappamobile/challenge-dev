using System.Linq;

namespace Wappa.Dominio.Resultado
{
    public class BuscaResultado<T> where T : class
    {
        public int TotalPaginas { get; set; }
        public IQueryable<T> Resultado { get; set; }
    }
}