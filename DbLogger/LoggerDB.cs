using System;
using System.Data.Entity;
using DBMaster.Entries;
using Logic.Components;

namespace DbLogger
{
    public class LoggerDB: ILogger
    {
        public LoggerDB()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RepositoryDBContext>());
        }
        public void Trace(string message)
        {
            using (RepositoryDBContext dbContext = new RepositoryDBContext())
            {
                dbContext.Log.Add(new LogRecord()
                {
                    Message = message,
                    Time = DateTime.Now.ToLocalTime(),
                    LogType = LogType.TraceLog
                });
                dbContext.SaveChanges();
            }
        }

        public void Info(string message)
        {
            using (RepositoryDBContext dbContext = new RepositoryDBContext())
            {
                dbContext.Log.Add(new LogRecord()
                {
                    Message = message,
                    Time = DateTime.Now.ToLocalTime(),
                    LogType = LogType.InfoLog
                });
                dbContext.SaveChanges();
            }
        }
    }
}