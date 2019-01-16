using System.Data.Entity;
using DBMaster.Entries;

namespace DbLogger
{
    public class RepositoryDBContext : DbContext
    {

        public RepositoryDBContext() : base("name=LogsDatabase")
        {

        }

        public virtual DbSet<LogRecord> Log { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
