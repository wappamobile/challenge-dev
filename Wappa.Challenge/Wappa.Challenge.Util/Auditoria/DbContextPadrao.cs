using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Wappa.Challenge.Util.Auditoria
{
    public class DbContextPadrao : DbContext
    {
        #region Construtores

        public DbContextPadrao() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Util.Conexao.SqlServer.StringDeConexao);
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Propriedade que guarda a string de conexão com a base dados
        /// </summary>
        public string StringConexao { get; set; }

        #endregion

        #region Variáveis

        private string conexaoAuditoria = string.Empty;

        #endregion

        #region Métodos
        
        public override int SaveChanges()
        {
            List<string> listAdicionados = new List<string>();
            AuditoriaDadosBLL acaoBLL = new AuditoriaDadosBLL();
            foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                if (ent.State == EntityState.Added)
                    listAdicionados.Add(RetornarNomeTabela(ent));
                else
                    acaoBLL.Gravar(RetornarAcao(ent));
            }
            int i = base.SaveChanges();
            foreach (var ent in this.ChangeTracker.Entries())
            {
                if (listAdicionados.Contains(RetornarNomeTabela(ent)))
                {
                    ent.State = EntityState.Added;
                    acaoBLL.Gravar(RetornarAcao(ent));
                }
            }
            return i;
        }

        private Auditoria RetornarAcao(EntityEntry pDBEntry)
        {
            string tableName = RetornarNomeTabela(pDBEntry);
            string[] chaves = pDBEntry.Entity.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).ToList().Select(item => item.Name).ToArray();

            Auditoria acao = new Auditoria();
            acao.DataHora = DateTime.Now;
            acao.Tabela = tableName;

            string acaoTipo = string.Empty;
            if (pDBEntry.State == EntityState.Added)
                acaoTipo = "I";
            else if (pDBEntry.State == EntityState.Deleted)
                acaoTipo = "E";
            else if (pDBEntry.State == EntityState.Modified)
                acaoTipo = "A";

            StringBuilder strNovos = new StringBuilder();
            StringBuilder strVelhos = new StringBuilder();
            StringBuilder strChaves = new StringBuilder();

            PropertyValues valores = acaoTipo == "E" ? pDBEntry.OriginalValues : pDBEntry.CurrentValues;
            foreach (string propertyName in valores.Properties.Select(s => s.PropertyInfo.Name))
            {
                if (chaves.Contains(propertyName))
                    strChaves.Append(propertyName + "=" + FormatarTipo(valores.GetValue<long>(propertyName)) + ";");
                else
                {
                    if (acaoTipo != "E")
                        strNovos.Append(propertyName + "=" + FormatarTipo(pDBEntry.CurrentValues[propertyName]) + ";");
                    if (acaoTipo != "I")
                        strVelhos.Append(propertyName + "=" + FormatarTipo(pDBEntry.CurrentValues[propertyName]) + ";");
                }
            }
            acao.ChavesPrimarias = strChaves.ToString();

            acao.ValorAntigo = strVelhos.ToString();
            acao.ValorNovo = strNovos.ToString();

            return acao;
        }
        
        private string RetornarNomeTabela(EntityEntry pDBEntry)
        {
            TableAttribute tableAttr = pDBEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
            return tableAttr != null ? tableAttr.Name : (pDBEntry.Entity.GetType().Name.Length > 50 ? pDBEntry.Entity.GetType().BaseType.Name : pDBEntry.Entity.GetType().Name);
        }

        private string FormatarTipo(object obj)
        {
            if (obj == System.DBNull.Value)
                return "NULL";
            else if (obj == null)
                return "NULL";
            else if (obj is bool)
                return Convert.ToBoolean(obj) == true ? "1" : "0";
            else if (obj is DateTime)
                return Convert.ToDateTime(obj).ToString("yyyy-MM-dd HH:mm:ss");
            else
                return obj.ToString();
        }

        #endregion
    }
}
