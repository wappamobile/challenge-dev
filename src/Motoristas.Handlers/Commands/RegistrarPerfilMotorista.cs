using MediatR;
using Motoristas.Handlers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motoristas.Handlers.Commands
{
    public class RegistrarPerfilMotorista : IRequest<RegistrarPerfilMotoristaResponse>
    {
        public RegistrarPerfilMotorista(MotoristaModel motorista)
        {
            Motorista = motorista;
        }

        public MotoristaModel Motorista { get; }
    }
}
