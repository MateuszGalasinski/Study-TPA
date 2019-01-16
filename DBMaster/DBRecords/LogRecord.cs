using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DBMaster.Entries
{
    public class LogRecord
    {
        public int LogRecordId { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public string CallerName { get; set; }
    }
}
