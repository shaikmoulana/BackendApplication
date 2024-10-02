using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Interviews : InterviewsDTO
    {
        public string? SOWRequirementId { get; set; }
        public string? StatusId { get; set; }

        [ForeignKey("SOWRequirementId")]
        public SOWRequirement SOWRequirement { get; set; }

        [ForeignKey("Recruiter")]
        public Employee Employee { get; set; }

        [ForeignKey("StatusId")]
        public InterviewStatus Status { get; set; }

    }
    public class InterviewsDTO : AuditData
    {
        public string? SOWRequirement { get; set; }
        public string? Name { get; set; }
        public DateTime? InterviewDate { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? Status { get; set; }
        public DateTime? On_Boarding { get; set; }
        public string? Recruiter { get; set; }

    }
}
