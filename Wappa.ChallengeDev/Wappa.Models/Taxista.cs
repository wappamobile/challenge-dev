using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wappa.Models
{
    public class Taxista
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdTaxista
        {
            get;
            set;
        }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        [Required(ErrorMessage = "Por favor informe o primeiro nome do taxista")]
        public string PrimeiroNome
        {
            get;
            set;
        }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        [Required(ErrorMessage = "Por favor informe o último nome do taxista")]
        public string UltimoNome
        {
            get;
            set;
        }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        [Required(ErrorMessage = "Por favor informe o marca do veículo")]
        public string Marca
        {
            get;
            set;
        }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        [Required(ErrorMessage = "Por favor informe o modelo do veículo")]
        public string Modelo
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Por favor informe a placa do veículo")]
        [Column(TypeName = "VARCHAR(8)")]
        [StringLength(8)]
        public string Placa
        {
            get;
            set;
        }

        [Column(TypeName = "VARCHAR(200)")]
        [StringLength(200)]
        [Required(ErrorMessage = "Por favor informe o endereço completo do taxista")]
        public string Endereco
        {
            get;
            set;
        }

        [Column(TypeName = "VARCHAR(25)")]
        public string Latitude
        {
            get;
            set;
        }

        [Column(TypeName = "VARCHAR(25)")]
        public string Longitude
        {
            get;
            set;
        }
    }
}
