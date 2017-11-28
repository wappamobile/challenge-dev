using System.ComponentModel.DataAnnotations;

namespace WappaChallenge.DTO
{
    public class CoordenadaGeograficaDTO : BaseDTO
    {
        [Required]
        public float Latitude { get; set; }

        [Required]
        public float Longitude { get; set; }
    }
}
