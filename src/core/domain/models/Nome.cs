using System;
using System.Linq;

namespace WappaMobile.ChallengeDev.Models
{
    public struct Nome
    {
        public string Primeiro, Ultimo;
        public string Completo => $"{Primeiro} {Ultimo}";

        public Nome(string primeiro, string ultimo)
        {
            if(string.IsNullOrEmpty(primeiro) || !Assertion.IsAllLetters(primeiro))
                throw new ArgumentException(nameof(primeiro));

            if(string.IsNullOrEmpty(ultimo) || !Assertion.IsAllLetters(ultimo))
                throw new ArgumentException(nameof(ultimo));

            Primeiro = primeiro;
            Ultimo = ultimo;
        }

        public Nome(string nomecompleto)
        {
            if(string.IsNullOrEmpty(nomecompleto) || !Assertion.IsAllLetters(nomecompleto))
                throw new ArgumentException(nameof(nomecompleto));

            var nomes = nomecompleto.Split(' ');

            Ultimo = string.Empty;

            if(nomes.Length == 1)
            {
                Primeiro = nomes[0];
            }
            else if(nomes.Length == 2)
            {
                Primeiro = nomes[0];
                Ultimo = nomes[1];
            }
            else if(nomes.Length == 4)
            {
                Primeiro = $"{nomes[0]} {nomes[1]}";
                Ultimo = $"{nomes[2]} {nomes[3]}";
            }
            else
            {
                Primeiro = nomes[0];
                Ultimo = string.Join(" ", nomes.Skip(1));
            }
        }

        public override bool Equals(object obj)
        {
            return ToString().Equals(obj.ToString());
        }

        public override string ToString()
        {
            return Completo;
        }

        public static implicit operator string(Nome nome)
        {
            return nome.Completo;
        }
    }
}
