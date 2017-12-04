using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Domain.Common;
using Wappa.Domain.Entities;

namespace Wappa.Domain.Repositories
{
    public interface IMotoristaRepository
    {
        Motorista Adicionar(Motorista motorista);
        Motorista Atualizar(Motorista motorista);
        void Excluir(int id);
        Motorista[] Listar(OrdenarPor ordernarPor);
        Motorista Obter(int id);
    }
}
