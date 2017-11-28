using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WappaChallenge.Dominio.Exceptions
{
    public class EntidadeInvalidaException : Exception
    {
        public EntidadeInvalidaException(Type tipoEntidade, ICollection<ValidationResult> erros)
        {
            StringBuilder mensagem = new StringBuilder();

            foreach (ValidationResult erro in erros)
                mensagem.AppendLine(erro.ErrorMessage);

            throw new EntidadeInvalidaException(mensagem.ToString());
        }

        private EntidadeInvalidaException(string mensagem) : base(mensagem) { }
    }
}
