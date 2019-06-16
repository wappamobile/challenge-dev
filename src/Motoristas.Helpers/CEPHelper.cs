using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Motoristas.Helpers
{
    public static class CEPHelper
    {
        public static bool Validar(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return false;

            return Regex.IsMatch(cep, @"\d{5}[-]{0,1}\d{3}");
        }
    }
}
