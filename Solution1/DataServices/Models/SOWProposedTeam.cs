using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class SOWProposedTeam
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? SOWRequirement { get; set; }

        public string? Employee { get; set; }

        public bool IsActive { get; set; } = true;

        public string CreatedBy { get; set; } = "SYSTEM";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        [ForeignKey("SOWRequirement")]
        public SOWRequirement SOWRequirements { get; set; }
        [ForeignKey("Employee")]
        public Employee Employees { get; set; }
    }

    public class SOWProposedTeamDTO
    {
        public string Id { get; set; }
        public string? SOWRequirement { get; set; }

        public string? Employee { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
