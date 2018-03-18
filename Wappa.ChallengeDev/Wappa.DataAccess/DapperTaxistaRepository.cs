using System;
using System.Collections.Generic;
using System.Text;
using Wappa.DataAccess.Contracts;
using Wappa.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Wappa.DataAccess
{
    public class DapperTaxistaRepository : ITaxistaRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;
        public DapperTaxistaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:Dapper"];
        }
        public int Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"DELETE FROM Taxista                                
                                    WHERE IdTaxista = @Id";
                int rowsAffected = db.Execute(sqlQuery, new { id });
                return rowsAffected;
            }
        }

        public Taxista Find(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Taxista>("SELECT * FROM Taxista where IdTaxista = @Id", new { id }).SingleOrDefault();
            }
        }

        public int Insert(Taxista taxista)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"INSERT INTO Taxista
                                        (Nome,Cpf,DataNascimento,Marca,Modelo,Placa,Ano,Chassi)
                                    VALUES
                                        (@Nome,@Cpf,@DataNascimento,@Marca,@Modelo,@Placa,@Ano,@Chassi)
                                    SELECT CAST(SCOPE_IDENTITY() as int)";
                return db.Query<int>(sqlQuery, taxista).Single();
            }
        }

        public List<Taxista> List()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Taxista>("SELECT * FROM Taxista ORDER BY Nome").ToList();
            }
        }
        public int GetTotal()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<int>("SELECT COUNT(*) FROM Taxista").FirstOrDefault();
            }
        }

        public int Update(Taxista taxista)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"UPDATE Taxista 
                                    SET Nome = @Nome,  
                                        Cpf = @Cpf,
                                        DataNascimento = @DataNascimento,
                                        Marca = @Marca,
                                        Modelo = @Modelo,
                                        Placa = @Placa,
                                        Ano = @Ano,
                                        Chassi = @Chassi                                
                                    WHERE IdTaxista = @IdTaxista";
                int rowsAffected = db.Execute(sqlQuery, taxista);
                return rowsAffected;
            }
        }
        public List<Taxista> PagedList(int pageSize, int pageNumber, int order, int orderby)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Taxista>(@"SELECT *
                      FROM Taxista
                      ORDER BY NOME
                      OFFSET @pageSize * (@pageNumber - 1) ROWS
                     FETCH NEXT @pageSize ROWS ONLY;", new { pageSize, pageNumber }).ToList();
            }
        }
    }
}
