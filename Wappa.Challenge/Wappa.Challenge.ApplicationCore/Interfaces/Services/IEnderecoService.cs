using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Challenge.ApplicationCore.Entities;

namespace Wappa.Challenge.ApplicationCore.Interfaces.Services
{
    public interface IEnderecoService : IBaseService<Endereco>
    {
        bool Apagar(long id);
    }
}
