﻿using DataServices.Models;
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
    public class Technology : TechnologyDTO
    {
        [Required]
        public string? DepartmentId { get; set; }
        public ICollection<ProjectTechnology> ProjectTechnologies { get; set; }
        public ICollection<EmployeeTechnology> EmployeeTechnology { get; set; }
        [NotMapped] // Add this attribute to ignore in EF Core
        [JsonIgnore] // Add this attribute to ignore during JSON deserialization
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
    public class TechnologyDTO : AuditData
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }
        public string? Department { get; set; }
    }
}