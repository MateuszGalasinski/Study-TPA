using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBMaster.Entries;

namespace DBMaster
{
    public class ReflectionDBContext : DbContext
    {
        
        

        public ReflectionDBContext() :base("Server=tcp:studytpa.database.windows.net,1433;Initial Catalog=StudyTPA;Persist Security Info=False;User ID=MaciejGala123;Password=studyTPA@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        {
            
        }

        public virtual DbSet<LogRecord> Log { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
