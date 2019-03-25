using System;
using System.Collections.Generic;
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

            //var motorista = _dbContext.Motoristas.Find(motorista.Id);
            return true;
        }

        public bool DeleteCadastro(int id)
        {
            var motorista = _dbContext.Motoristas.Find(id);
            _dbContext.Motoristas.Remove(motorista);
            return _dbContext.SaveChanges() > 0;
        }

    }
}
