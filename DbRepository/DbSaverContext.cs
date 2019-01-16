using System.Data.Entity;
using DbRepository.Models;

namespace DbRepository
{
    public class DbSaverContext : DbContext
    {
        public DbSaverContext() : base("name=AssemblyDatabase")
        {

        }

        public virtual DbSet<AssemblyDbSaver> AssemblyDbSavers { get; set; }
        public virtual DbSet<MethodDbSaver> MethodDbSavers { get; set; }
        public virtual DbSet<NamespaceDbSaver> NamespaceDbSavers { get; set; }
        public virtual DbSet<ParameterDbSaver> ParameterDbSavers { get; set; }
        public virtual DbSet<PropertyDbSaver> PropertyDbSavers { get; set; }
        public virtual DbSet<TypeDbSaver> TypeDbSavers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
