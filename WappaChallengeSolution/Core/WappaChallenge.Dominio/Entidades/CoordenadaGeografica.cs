using System.ComponentModel.DataAnnotations;

namespace WappaChallenge.Dominio.Entidades
{
    public class CoordenadaGeografica : BaseDominio
    {
        [Required]
        public float Latitude { get; protected set; }

        [Required]
        public float Longitude { get; protected set; }

        public CoordenadaGeografica(float latitude, float longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;

            this.ValidarEntidade();
        }
    }
}
