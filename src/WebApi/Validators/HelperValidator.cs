using System;
using System.Text.RegularExpressions;

namespace WebApi.Validators
{
    public static class HelperValidator
    {
        public static bool ValidNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            input = Regex.Replace(input, @"[^0-9]", string.Empty);
            int n;
            return Int32.TryParse(input, out n);
        }
    }
}
