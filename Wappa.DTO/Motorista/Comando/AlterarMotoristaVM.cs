using System.Collections.Generic;

namespace Wappa.ViewModel.Motorista.Comando
{
    public class AlterarMotoristaVM
    {
        public AlterarMotoristaVM(int id,
                                string nome,
                                string sobrenome,
                                ICollection<EnderecoVM> enderecoLista,
                                ICollection<CarroVM> carroLista)
        {
            this.Id = id;
            this.Nome = nome;
            this.SobreNome = sobrenome;
            this.EnderecoLista = enderecoLista;
            this.CarroLista = carroLista;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public ICollection<EnderecoVM> EnderecoLista { get; set; }
        public ICollection<CarroVM> CarroLista { get; set; }
    }
}