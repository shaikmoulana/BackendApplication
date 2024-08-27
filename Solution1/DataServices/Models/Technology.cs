using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Technology
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string? DepartmentId { get; set; }

        public bool IsActive { get; set; } = true;

        public string CreatedBy { get; set; } = "SYSTEM";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; } // Make this nullable
        public ICollection<ProjectTechnology> ProjectTechnologies { get; set; }
        public ICollection<EmployeeTechnology> EmployeeTechnology { get; set; }
        [NotMapped] // Add this attribute to ignore in EF Core
        [JsonIgnore] // Add this attribute to ignore during JSON deserialization
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
    public class TechnologyDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        public string? Department { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ?UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}