using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class CarroNaoEncontradoException : Exception
    {
        public CarroNaoEncontradoException() : base("Carro não encontrado")
        {

        }
    }
}
