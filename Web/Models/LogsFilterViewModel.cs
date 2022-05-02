using Logs.BLL.Enums;

namespace Web.Models
{
    public class LogsFilterViewModel
    {
        public LogType LogType { get; set; }
        public string Keywords { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;

        public LogsFilterViewModel()
        {
        }
    }
}
