using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wappa.Framework.Model.Enum
{
    public class Ordenacao
    {
        [EnumDataType(typeof(TipoOrdenacao))]
        public TipoOrdenacao TipoOrdenacao { get; set; }
    }

    public enum TipoOrdenacao
    {
        Nome,
        Sobrenome
    }
}
