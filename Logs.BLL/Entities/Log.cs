using System;

namespace Logs.BLL.Entities
{
    public class Log : BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }

        public int LogTypeId { get; set; }
        public LogType LogType { get; set; }
    }
}
