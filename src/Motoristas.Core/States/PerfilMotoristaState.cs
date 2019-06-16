using System;
using System.Collections.Generic;

namespace Motoristas.Core.States
{
    public class PerfilMotoristaState : IStateIdentity<string>
    {
        public string Id { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public MotoristaState Motorista { get; set; }
        public DateTimeOffset DataUltimaAtualizacao { get; set; }
    }
}
