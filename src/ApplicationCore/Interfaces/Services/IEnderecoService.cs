using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces.Services
{
    public interface IEnderecoService
    {
        void Add(Endereco endereco);
        void Update(Endereco endereco);
    }
}
