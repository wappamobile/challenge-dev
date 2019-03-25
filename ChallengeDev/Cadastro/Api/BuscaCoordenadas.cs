using System;
using System.Collections.Generic;
using System.Text;
using Cadastro.Entities;

namespace Cadastro.Api
{
   public class BuscaCoordenadas
    {
        private readonly Endereco _endereco;

        public BuscaCoordenadas( Endereco endereco)
        {
            _endereco = endereco;
        }


        public Endereco RetornaEnderecoComCoordenadas(Endereco endereco)
        {
           return new Endereco();
        }
   }
}
