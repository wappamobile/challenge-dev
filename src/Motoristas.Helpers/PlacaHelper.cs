using System.Text.RegularExpressions;

namespace Motoristas.Helpers
{
    public static class PlacaHelper
    {
        public static bool Validar(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return false;

            return Regex.IsMatch(placa, @"^[A-Z]{3}\d[\d|A-J]\d{2}$", RegexOptions.IgnoreCase);
        }
    }
}
