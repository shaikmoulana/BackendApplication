using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class SOWRequirement
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? SOW { get; set; }
        public string? DesignationId { get; set; }
        public string? Technologies { get; set; }
        public int? TeamSize { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<Interviews> Interviews { get; set; }
        public ICollection<SOWProposedTeam> SOWProposedTeam { get; set; }
        [ForeignKey("SOW")]
        public SOW SOWs { get; set; }
        [ForeignKey("DesignationId")]
        public Designation Designation { get; set; }

    }

    public class SOWRequirementDTO
    {
        public string Id { get; set; }
        public string? SOW { get; set; }
        public string? Designation { get; set; }
        public string? Technologies { get; set; }
        public int? TeamSize { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
