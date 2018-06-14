using System;
using System.Collections.Generic;
using System.Text;
using Teste.Repositorio.Interface;

namespace Teste.Implementacao.DTO.Extensao
{
    public static class EntidadeExtension
    {
        public static bool EstaValido<T>(this T entidade) where T : IEntidade
        {
            return (entidade != null);
        }

    }
}
