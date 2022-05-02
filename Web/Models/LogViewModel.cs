using Logs.BLL.Entities;
using System;

namespace Web.Models
{
    public class LogViewModel
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public static LogViewModel FromLog(Log item)
        {
            return new LogViewModel
            {
                Id = item.Id,
                Date = item.CreatedAt,
                Description = item.Description,
                Type = item.LogType.Name
            };
        }
    }
}
