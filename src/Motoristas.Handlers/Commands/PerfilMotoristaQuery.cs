using MediatR;
using Motoristas.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motoristas.Handlers.Commands
{
    public class PerfilMotoristaQuery : IRequest<PerfilMotoristaQueryResponse>, IFiltroPerfilMotorista
    {
        public string Sort { get; set; }
    }
}
