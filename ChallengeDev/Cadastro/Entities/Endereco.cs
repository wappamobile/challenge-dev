using System;
using System.Collections.Generic;
using System.Text;

namespace Cadastro.Entities
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string cep { get; set; }

        public virtual Coordenada Coordenada { get; set; }

    }
}
