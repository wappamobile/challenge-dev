using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motoristas.Handlers.Commands
{
    public class RemoverPerfilMotorista : IRequest<RemoverPerfilMotoristaResponse>
    {
        public RemoverPerfilMotorista(string perfilId)
        {
            PerfilId = perfilId;
        }

        public string PerfilId { get; }
    }
}
