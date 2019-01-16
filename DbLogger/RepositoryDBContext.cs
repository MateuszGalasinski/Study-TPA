using System.Data.Entity;
using DBMaster.Entries;
using System.Data.SqlClient;

namespace DbLogger
{
    public class RepositoryDBContext : DbContext
    {

        public RepositoryDBContext() : base("name=AssemblyDatabase")
        {

        }

        public virtual DbSet<LogRecord> Log { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
