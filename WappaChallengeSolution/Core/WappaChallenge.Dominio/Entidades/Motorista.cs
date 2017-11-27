using System.ComponentModel.DataAnnotations;

namespace WappaChallenge.Dominio.Entidades
{
    public class Motorista : BaseDominio<int>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O primeiro nome é obrigatório.")]
        public string PrimeiroNome { get; protected set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O primeiro nome é obrigatório.")]
        public string UltimoNome { get; protected set; }

        [Required(ErrorMessage = "O veículo é obrigatório.")]
        public Veiculo Veiculo { get; protected set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public Endereco Endereco { get; protected set; }

        public Motorista(string primeiroNome, string ultimoNome, Veiculo veiculo, Endereco endereco)
        {
            this.PrimeiroNome = primeiroNome;
            this.UltimoNome = ultimoNome;
            this.Veiculo = veiculo;
            this.Endereco = endereco;

            this.ValidarEntidade();
        }
    }
}
