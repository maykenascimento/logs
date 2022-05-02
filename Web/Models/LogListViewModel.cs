using System.Collections.Generic;

namespace Web.Models
{
    public class LogListViewModel
    {
        public List<LogViewModel> Logs { get; set; } = new List<LogViewModel>();
        public List<LogTypeViewModel> Types { get; set; } = new List<LogTypeViewModel>();

        public LogListViewModel(List<LogViewModel> logs, List<LogTypeViewModel> types)
        {
            if (logs != null && logs.Count > 0)
            {
                Logs = logs;
            }

            if (logs != null && logs.Count > 0)
            {
                Types = types;
            }
        }
    }
}
