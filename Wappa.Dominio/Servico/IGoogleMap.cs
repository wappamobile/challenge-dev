using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Dominio.Entidade;
using Wappa.Dominio.Resultado;

namespace Wappa.Dominio.Servico
{
    public interface IGoogleMap
    {
        ResuladoGoogleMap ObterLocalidade(Endereco endereco);
    }
}
