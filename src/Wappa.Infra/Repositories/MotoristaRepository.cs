using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;  
using Wappa.Core.Interfaces;
using Wappa.Core.Models;

namespace Wappa.Infra.Repositories
{
    public class MotoristaRepository : IMotoristaRepository
    {
        private readonly string _connectionString;

        public MotoristaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<int> Delete(int id)
        {
            using(var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Motorista>("motoristas");            
                var result = col.Delete(q => q.Id == id);
            }

            return Task.FromResult(id);     
        }

        public Task<Motorista> Get(int id)
        {
            Motorista result = null;

            using(var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Motorista>("motoristas");            
                result = col.FindById(id);
            }

            return Task.FromResult(result);  
        }

        public Task<IEnumerable<Motorista>> GetAll(string ordem)
        {
            IEnumerable<Motorista> result = null;

            using(var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Motorista>("motoristas");            
                result = col.FindAll();
            }

            if (result == null) return null;

            if (string.IsNullOrEmpty(ordem)) 
            {
                result = result.OrderBy(q => q.Id);
            }
            else
            {
                result = ordem == "PrimeiroNome" 
                    ? result.OrderBy(q => q.PrimeiroNome) 
                    : result.OrderBy(q => q.UltimoNome);
            }

            return Task.FromResult(result);
        }

        public Task<Motorista> Save(Motorista motorista)
        {
            using(var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Motorista>("motoristas");            
                var result = col.Insert(motorista);
            }

            return Task.FromResult(motorista);
        }

        public Task<Motorista> Update(Motorista motorista)
        {
            using(var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Motorista>("motoristas");            
                col.Update(motorista);
            }

            return Task.FromResult(motorista);
        } 
    }
}