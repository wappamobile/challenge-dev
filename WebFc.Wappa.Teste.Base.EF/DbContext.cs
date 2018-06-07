using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFC.Wappa.Teste.Base.Core.Models;

namespace WebFc.Wappa.Teste.Base.EF
{
    public partial class MotoristasContext : DbContext
    {

        static MotoristasContext()
        {
            // Ensure DLL is copied for no need reference EF in Web project
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            Database.SetInitializer<MotoristasContext>(null);
        }

        public MotoristasContext()
            : base("name=MotoristasContext")
        {
        }

        public MotoristasContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            // Ensure DLL is copied for no need reference EF in Web project
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180000;

        }

        #region DBSet
        public DbSet<Motoristas> Motoristas  { get; set; }

        public DbSet<Carros> Carros { get; set; }

        
        #endregion



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}
