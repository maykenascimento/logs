using System;
using Logs.BLL.Enums;
using Logs.BLL.Entities;

namespace Web.Models
{
    public class AuditModel
    {
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string TableName { get; set; }
        public int? PrimaryKey { get; set; }
        public AuditType AuditType { get; set; }

        public static Audit ToAudit(string description, string tableName, int? primaryKey, AuditType type)
        {
            return new Audit
            {
                CreatedAt = DateTime.UtcNow,
                Description = description,
                TableName = tableName,
                PrimaryKey = primaryKey,
                AuditType = type
            };
        }
    }
}
