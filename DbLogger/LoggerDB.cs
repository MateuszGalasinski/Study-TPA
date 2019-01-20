using DBMaster.Entries;
using Logic.Components;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;

namespace DbLogger
{
    [Export(typeof(ILogger))]
    public class LoggerDB : ILogger
    {
        public LoggerDB()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<RepositoryDBContext>());
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