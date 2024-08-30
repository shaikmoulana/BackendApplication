using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Interviews
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? SOWRequirementId { get; set; }
        public string? Name { get; set; }
        public DateTime? InterviewDate { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? StatusId { get; set; }
        public DateTime? On_Boarding { get; set; }
        public string? Recruiter { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("SOWRequirementId")]
        public SOWRequirement SOWRequirements { get; set; }

        [ForeignKey("Recruiter")]
        public Employee Employee { get; set; }

        [ForeignKey("StatusId")]
        public InterviewStatus InterviewStatus { get; set; }

    }
    public class InterviewsDTO
    {
        public string Id { get; set; }
        public string? SOWRequirement { get; set; }
        public string? Name { get; set; }
        public DateTime? InterviewDate { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? Status { get; set; }
        public DateTime? On_Boarding { get; set; }
        public string? Recruiter { get; set; }
        public bool IsActive { get; set; } 
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } 
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
