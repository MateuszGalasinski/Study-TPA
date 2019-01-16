using System;
using DbLogger;

namespace DBMaster.Entries
{
    public class LogRecord
    {
        public int LogRecordId { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public LogType LogType { get; set; }
    }
}