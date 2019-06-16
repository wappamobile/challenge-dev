using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motoristas.Handlers.Commands
{
    public class ObterPerfilMotorista : IRequest<ObterPerfilMotoristaResponse>
    {
        public ObterPerfilMotorista(string perfilId)
        {
            PerfilId = perfilId;
        }

        public string PerfilId { get; }
    }
}
