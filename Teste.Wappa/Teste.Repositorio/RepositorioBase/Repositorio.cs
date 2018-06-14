using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Teste.Repositorio.Interface;
using Teste.Repositorio.Entidade;

namespace Teste.Repositorio.RepositorioBase
{
    public abstract class Repositorio<T> : IRepositorio<T>, IDisposable
    {
        private string ObterStringConexaoSqlServer() {
            return "Server=den1.mssql6.gear.host;Database=takatest;User Id=takatest;Password=Yv8K_CcC9?1l;";
        }
        
        public SqlConnection ConexaoSqlServer {
            get {
                var connstr = ObterStringConexaoSqlServer();
                SqlConnection xpto = new SqlConnection(connstr);
                return xpto;
            }
        }
        
        public abstract IEnumerable<T> Consultar();
        public abstract void Excluir(int id);
        public abstract void Inserir(T dados);
        public abstract void Editar(T dados);

        public void Dispose()
        {
            ConexaoSqlServer.Close();
        }
    }
}
