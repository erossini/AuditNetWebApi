using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.Interfaces
{
    public interface IAudit
    {
        string AuditAction { get; set; }
        DateTime AuditDate { get; set; }
        string UserName { get; set; }
    }
}