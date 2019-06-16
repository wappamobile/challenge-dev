using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Motoristas.Core
{
    public class Endereco
    {

        public Endereco(string logradouro,
                        string bairro,
                        string cidade,
                        string uf,
                        string cep,
                        string descricao,
                        string numero,
                        string complemento,
                        Coordenadas coordenadas)
        {
            Logradouro = logradouro;
            Cidade = cidade;
            Cep = cep ?? throw new ArgumentNullException(nameof(cep));
            Bairro = bairro;
            Descricao = descricao;
            Numero = numero;
            Complemento = complemento;
            Coordenadas = coordenadas;
            Uf = uf; 
        }

        public string Logradouro { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string Uf { get; }
        public string Cep { get; }
        public string Descricao { get; }
        public string Numero { get; }
        public string Complemento { get; }
        public Coordenadas Coordenadas { get; }

        public static bool MesmaCidade(Endereco a, Endereco b)
        {
            return a.Uf.Equals(b.Uf)
                   && a.Cidade.RemoveDiacritics().ToUpperInvariant().Equals(b.Cidade.RemoveDiacritics().ToUpperInvariant());
        }
    }

    public class Coordenadas
    {
        public Coordenadas(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; }
        public double Longitude { get; }
    }

    public static class RemoveDiacritcs
    {
        public static string RemoveDiacritics(this string input)
        {
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}