using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Models;

namespace Wappa.DataAccess
{
    public class WappaContext : DbContext
    {
        public WappaContext(DbContextOptions<WappaContext> options) : base(options)
        {
        }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
    }
}
