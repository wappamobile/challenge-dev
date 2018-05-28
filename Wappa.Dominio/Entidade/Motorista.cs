using System.Collections.Generic;

namespace Wappa.Dominio.Entidade
{
    public class Motorista
    {
        public Motorista()
        {
            ListaEndereco = new HashSet<Endereco>();
            ListaCarro = new HashSet<Carro>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public ICollection<Endereco> ListaEndereco { get; set; }
        public ICollection<Carro> ListaCarro { get; set; }
    }
}