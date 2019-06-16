using System;

namespace Motoristas.Core
{
    public class Veiculo
    {
        public Veiculo(        string placa,
                               string marca,
                               string modelo)
        {
            Placa = placa ?? throw new ArgumentNullException(nameof(placa));
            Marca = marca ?? throw new ArgumentNullException(nameof(marca));
            Modelo = modelo ?? throw new ArgumentNullException(nameof(modelo));
        }

        public string Marca { get; }
        public string Modelo { get; }
        public string Placa { get; }
    }
}