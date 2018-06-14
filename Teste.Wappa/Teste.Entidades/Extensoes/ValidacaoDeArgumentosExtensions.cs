using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Entidades.Extensoes
{
    public static class ValidacaoDeArgumentosExtensions
    {
        public static T ValidaAtributo<T>(this T o, string paramName)
        {
            if (o == null)
                throw new ArgumentException(paramName);

            return o;
        }

    }
}
