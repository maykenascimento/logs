using System;
using Logs.BLL.Entities;
using Logs.BLL.Enums;

namespace Web.Models
{
    public class AuditViewModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }

        public static AuditViewModel FromAudit(Audit item)
        {
            return new AuditViewModel
            {
                Date = item.CreatedAt,
                Source = item.TableName,
                Description = item.Description,
                Type = GetType(item.AuditType)
            };
        }

        private static string GetType(AuditType type)
        {
            switch (type)
            {
                case AuditType.View:
                    return "Viewed";
                case AuditType.Create:
                    return "Created";
                case AuditType.Update:
                    return "Updated";
                case AuditType.Delete:
                    return "Deleted";
                case AuditType.All:
                default:
                    return "Undefined";
            }
        }
    }
}
