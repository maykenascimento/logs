using System.Collections.Generic;

namespace Web.Models
{
    public class LogEditViewModel
    {
        public LogViewModel Log { get; set; }
        public List<LogTypeViewModel> Types { get; set; }

        public LogEditViewModel(LogViewModel log, List<LogTypeViewModel> types)
        {
            Log = log;
            Types = types;
        }
    }
}
