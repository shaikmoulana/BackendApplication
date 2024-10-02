using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class POCTeam : POCTeamDTO
    {
        public string? POCId { get; set; }
        public string? EmployeeId { get; set; }
        public POC? POC { get; set; }
        public Employee? Employee { get; set; }

    }
    public class POCTeamDTO : AuditData
    {
        public string? POC { get; set; }
        public string? Employee { get; set; }

    }
}
