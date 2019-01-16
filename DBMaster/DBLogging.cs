using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Components;
using DBMaster.Entries;

namespace DBMaster
{
    public class DBLogging : ILogger
    {
        public DBLogging()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ReflectionDBContext>());
        }
        public void Trace(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            using ( ReflectionDBContext dbContext = new ReflectionDBContext())
            {
                dbContext.Log.Add(new LogRecord()
                {
                    CallerName = "test",
                    Message = message,
                    Time = DateTime.Now.ToLocalTime()
                });
                dbContext.SaveChanges();
            }
        }
    }
}
