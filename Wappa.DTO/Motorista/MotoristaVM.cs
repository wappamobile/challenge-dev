using System.Collections.Generic;

namespace Wappa.ViewModel.Motorista
{
    public class MotoristaVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }

        public string NomeCompleto
        {
            get
            {
                return $"{Nome}/{SobreNome}";
            }
        }

        public ICollection<EnderecoVM> EnderecoLista { get; set; }
        public ICollection<CarroVM> CarroLista { get; set; }
    }
}