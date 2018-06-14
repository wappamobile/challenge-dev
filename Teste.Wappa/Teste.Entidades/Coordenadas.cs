using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Entidades
{
    public class Coordenadas
    {
        public Coordenadas(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}
