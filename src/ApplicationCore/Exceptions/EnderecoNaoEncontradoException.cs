using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class EnderecoNaoEncontradoException : Exception
    {
        public EnderecoNaoEncontradoException() : base("Endereço não encontrado")
        {

        }
    }
}
