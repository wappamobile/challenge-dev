using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Wappa.Challenge.Util.Auditoria
{
    /// <summary>
    /// Classe que faz o acesso a base de dados usando o entity framework
    /// </summary>
    internal partial class AuditoriaDados : DbContext
    {
        #region Construtores
        
        public AuditoriaDados() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Util.Conexao.SqlServer.StringDeConexao);
        }

        #endregion

        #region Propriedades

        public DbSet<Auditoria> Auditoria { get; set; }

        #endregion
    }
}
