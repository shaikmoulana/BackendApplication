using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class SOW : SOWDTO
    {
        public string? ClientId { get; set; }
        public string? ProjectId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        [ForeignKey("Status")]
        public SOWStatus SOWStatus { get; set; }
        public ICollection<SOWRequirement> SOWRequirement { get; set; }
        public ICollection<SOWRequirementTechnology> SOWRequirementTechnology { get; set; }

    }
    public class SOWDTO : AuditData
    {
        public string? Title { get; set; }
        public string? Client { get; set; }
        public string? Project { get; set; }
        public DateTime? PreparedDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string? Status { get; set; }
        public string? Comments { get; set; }
    }
}
