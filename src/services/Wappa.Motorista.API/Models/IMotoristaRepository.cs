﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Core.Data;

namespace Wappa.Motoristas.API.Models
{
    public interface IMotoristaRepository : IRepository<Motorista>
    {
        void Adicionar(Motorista cliente);

        Task<IEnumerable<Motorista>> ObterTodos();
    }
}