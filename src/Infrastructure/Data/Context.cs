using ApplicationCore.Entity;
using Infra.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<GeoLocation> GeoLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MotoristaMap());
            modelBuilder.ApplyConfiguration(new CarroMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
            modelBuilder.ApplyConfiguration(new GeoLocationMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
