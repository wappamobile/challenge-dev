using System.Collections.Generic;

namespace Driver.Application.Services.Entities
{
    /// <summary>
    /// Estrutura de resposta do Google
    /// </summary>
    public class GoogleMapsResult
    {
        /// <summary>
        /// Lista de resultados
        /// </summary>
        public List<Address> Results { get; set; }

        /// <summary>
        /// Status da chamada
        /// </summary>
        public string Status { get; set; }

        public class Address
        {
            /// <summary>
            /// Informação da posição do mapa
            /// </summary>
            public Geometry Geometry { get; set; }
        }

        public class Geometry
        {
            /// <summary>
            /// Coordenadas
            /// </summary>
            public Location Location { get; set; }
        }

        public class Location
        {
            /// <summary>
            /// Latitude
            /// </summary>
            public double Lat { get; set; }

            /// <summary>
            /// Longitude
            /// </summary>
            public double Lng { get; set; }
        }
    }
}