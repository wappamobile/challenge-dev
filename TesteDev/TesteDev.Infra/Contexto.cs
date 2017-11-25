using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TesteDev.Infra.Entidades;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TesteDev.Infra
{
    public class Contexto : DbContext
    {
        /// <summary>
        /// Construtor do contexto necessário para DI
        /// </summary>
        /// <param name="options">Opstions do contexto que é passado para o DbContext</param>
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        #region Configuração dos Objetos

        public DbSet<Motorista> DbMotoristas { get; set; }
        public DbSet<Carro> DbCarros { get; set; }
        public DbSet<EnderecoCompleto> DbEnderecos { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorista>().ToTable("Motorista").Property(a => a.Ativo).HasDefaultValueSql("1");

            modelBuilder.Entity<Carro>().ToTable("Carro");
            modelBuilder.Entity<Carro>().HasIndex(c => c.Placa).IsUnique();

            modelBuilder.Entity<EnderecoCompleto>().ToTable("EnderecoCompleto");
        }

        public override int SaveChanges()
        {
            AjustarDatas();

            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                //Tratar log com erros gerados
                throw ex;
            }
        }

        private void AjustarDatas()
        {
            var entities = ChangeTracker.Entries().Where(
                    r => (r.Entity is EntidadeBase)
                    && (r.State == EntityState.Added || r.State == EntityState.Modified)
                );

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntidadeBase)entity.Entity).DataCriacao = DateTime.Now;
                }

                ((EntidadeBase)entity.Entity).DataAlteracao = DateTime.Now;

                if (((EntidadeBase)entity.Entity).Ativo == true)
                    ((EntidadeBase)entity.Entity).DataRemovido = null;
            }
        }
    }
}
