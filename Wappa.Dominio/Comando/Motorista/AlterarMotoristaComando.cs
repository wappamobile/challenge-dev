using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;
using Wappa.Dominio.Entidade;

namespace Wappa.Dominio.Comando.Motorista
{
    public class AlterarMotoristaComando : Notifiable, IComando
    {
        private IList<Endereco> _enderecoLista;
        private IList<Carro> _carroLista;

        public AlterarMotoristaComando(int id,
                                       string nome,
                                       string sobrenome)
        {
            this.Id = id;
            this.Nome = nome;
            this.SobreNome = sobrenome;
            this._enderecoLista = new List<Endereco>();
            this._carroLista = new List<Carro>();
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string SobreNome { get; private set; }

        public ICollection<Endereco> EnderecoLista
        {
            get { return _enderecoLista; }
            private set { _enderecoLista = new List<Endereco>(value); }
        }

        public ICollection<Carro> CarroLista
        {
            get { return _carroLista; }
            private set { _carroLista = new List<Carro>(value); }
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            _enderecoLista.Add(validarEndereco(endereco));
        }

        public void AdicionarCarro(Carro carro)
        {
            _carroLista.Add(validarCarro(carro));
        }

        private Carro validarCarro(Carro carro)
        {
            AddNotifications(new Contract()
               .Requires()
               .IsTrue(carro.Id > 0, "Id", "Id carro é obrigatória")
               .IsNullOrEmpty(carro.Cor, "Cor", "Cor é obrigatória")
               .IsNullOrEmpty(carro.Placa, "Placa", "Placa é obrigatória")
               .IsNullOrEmpty(carro.Modelo, "Modelo", "Modelo é obrigatória"));

            return carro;
        }

        private Endereco validarEndereco(Endereco endereco)
        {
            AddNotifications(new Contract()
               .Requires()
               .IsTrue(endereco.Id > 0, "Id", "Id endereco é obrigatória")
               .HasMinLen(endereco.Cep.ToString(), 8, "Nome", "O nome deve conter pelo menos 8 caracteres")
               .IsNullOrEmpty(endereco.Logradouro, "Logradouro", "Logradouro é obrigatória"));

            return endereco;
        }

        public void Validar()
        {
            AddNotifications(new Contract()
                           .Requires()
                           .HasMinLen(Nome, 3, "Nome", "O nome deve conter pelo menos 3 caracteres")
                           .HasMaxLen(Nome, 55, "Nome", "O nome deve conter no máximo 55 caracteres")
                           .HasMinLen(SobreNome, 3, "SobreNome", "O sobreNome deve conter pelo menos 3 caracteres")
                           .HasMaxLen(SobreNome, 55, "SobreNome", "O sobreNome deve conter no máximo 55 caracteres"));
        }
    }
}