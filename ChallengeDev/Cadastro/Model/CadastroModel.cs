using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Cadastro.Data;
using Cadastro.Entities;
using Cadastro.Interface;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cadastro.Model
{
    public class CadastroModel : ICadastroModel
    {
        private  readonly AppDBContext _dbContext = new AppDBContext();

        public IEnumerable<Motorista> RetornaTodos()
        {
            try
            {
                return _dbContext.Motoristas;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
           
        }

        public bool NovoCadastro(Motorista novoMotorista)
        {
            try
            {
                _dbContext.Motoristas.Add(novoMotorista);
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                //TODO : IMPLEMENTAÇÃO DE LOG
                Console.WriteLine(e);
                throw;
            }
           
        }

        public bool AtualizaCadastro(Motorista motorista)
        {
            try
            {
                var result = _dbContext.Motoristas.Find(motorista.Id);
                result.Nome = motorista.Nome;
                result.Sobrenome = motorista.Sobrenome;
                result.Endereco = motorista.Endereco;
                result.Veiculo = motorista.Veiculo;

                _dbContext.Motoristas.Update(result);
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }

        public bool DeleteCadastro(int id)
        {
            try
            {
                var motorista = _dbContext.Motoristas.Find(id);
                _dbContext.Motoristas.Remove(motorista);
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

    }
}
