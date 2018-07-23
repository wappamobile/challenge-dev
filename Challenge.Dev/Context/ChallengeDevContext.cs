using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Challenge.Dev.Models;

namespace Challenge.Dev.Context
{
    public class ChallengeDevDbContext : DbContext
    {
        public ChallengeDevDbContext(DbContextOptions<ChallengeDevDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
