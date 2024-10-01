using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class SOWProposedTeam : SOWProposedTeamDTO
    {

        public string? SOWRequirementId { get; set; }

        public string? EmployeeId { get; set; }
        [ForeignKey("SOWRequirementId")]
        public SOWRequirement SOWRequirement { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }

    public class SOWProposedTeamDTO : AuditData
    {
        public string? SOWRequirement { get; set; }

        public string? Employee { get; set; }
    }
}
