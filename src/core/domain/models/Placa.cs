using System.Linq;

namespace WappaMobile.ChallengeDev.Models
{
    public struct Placa
    {
        string _letras;

        public string Letras { get { return _letras; } set { _letras = value.ToUpper(); } }
        public string Numeros { get; set; }

        public Placa(string placa)
        {
            _letras = placa.Substring(0, 3);
            Numeros = placa.Substring(placa.Length - 4);
        }

        public Placa(string letras, string numeros)
        {
            _letras = letras;
            Numeros = numeros;
        }

        public bool Valida
        {
            get
            {
                return Numeros.All(x => "0123456789".Contains(x));
            }
        }

        public override string ToString()
        {
            return $"{Letras}-{Numeros}";
        }
    }
}