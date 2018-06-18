using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class MotoristaNaoEncontradoException : Exception
    {
        public MotoristaNaoEncontradoException() : base("Motorista não encontrado")
        {

        }
    }
}
