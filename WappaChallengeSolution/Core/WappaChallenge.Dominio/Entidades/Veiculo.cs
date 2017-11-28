using System.ComponentModel.DataAnnotations;

namespace WappaChallenge.Dominio.Entidades
{
    public class Veiculo : BaseDominio
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "A marca é obrigatória.")]
        public string Marca { get; protected set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O modelo é obrigatória.")]
        public string Modelo { get; protected set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A placa é obrigatória.")]
        [RegularExpression(@"^[A-Z]{3}-\d{4}$", ErrorMessage = "Placa no formato inválido.")]
        public string Placa { get; protected set; }

        public Veiculo(string marca, string modelo, string placa)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.Placa = placa;

            this.ValidarEntidade();
        }
    }
}
