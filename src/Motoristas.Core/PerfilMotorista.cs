using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Motoristas.Core.Mappers;
using Motoristas.Core.States;

namespace Motoristas.Core
{

    public class PerfilMotorista : IAggregateRoot, IEntity<string>
    {
        private readonly PerfilMotoristaState _state;

        internal PerfilMotorista(PerfilMotoristaState state)
        {
            _state = state;
        }

        public PerfilMotorista(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

            _state = new PerfilMotoristaState
            {
                Id = id,
                DataCriacao = DateTimeOffset.Now
            };

            AtualizarDataModificacao();
        }

        #region Properties

        public string Id => _state.Id;
        public DateTimeOffset DataCriacao => _state.DataCriacao;
        public DateTimeOffset DataUltimaAtualizacao => _state.DataUltimaAtualizacao;
        public Motorista Motorista => _state.Motorista.ToDomain();

        #endregion

        private void AtualizarDataModificacao()
        {
            _state.DataUltimaAtualizacao = DateTimeOffset.Now;
        }

        public void DefinirMotorista(Motorista motorista)
        {
            if (motorista == null)
                throw new ArgumentNullException(nameof(motorista));

            _state.Motorista = motorista.ToState();
            AtualizarDataModificacao();
        }

        public void DefinirCoordenadasEnderco(Coordenadas coordenadas)
        {
            if (coordenadas == null)
                throw new ArgumentNullException(nameof(coordenadas));

            _state.Motorista.Endereco.Coordenadas = coordenadas.ToState();
        }

        public object GetState()
        {
            return _state;
        }
    }
}