using System;
using Logs.BLL.Enums;

namespace Logs.BLL.Entities
{
    public class Audit : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string TableName { get; set; }
        public int? PrimaryKey { get; set; }
        public AuditType AuditType { get; set; }
    }
}
