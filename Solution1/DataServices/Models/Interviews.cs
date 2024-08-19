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
        public string? SOWRequirement { get; set; }
        public string? Name { get; set; }
        public DateTime? InterviewDate { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? Status { get; set; }
        public DateTime? On_Boarding { get; set; }
        public string? Recruiter { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("SOWRequirement")]
        public SOWRequirement SOWRequirements { get; set; }

        [ForeignKey("Recruiter")]
        public Employee Recruiters { get; set; }

        [ForeignKey("Status")]
        public InterviewStatus Statuses { get; set; }

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
