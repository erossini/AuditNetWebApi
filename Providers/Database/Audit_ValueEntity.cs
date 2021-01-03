using Projects.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.Providers.Database
{
    public class Audit_ValueEntity : IAudit
    {
        [Key]
        public int AuditId { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }

        public string AuditAction { get; set; }
        public DateTime AuditDate { get; set; }
        public string UserName { get; set; }
    }
}
