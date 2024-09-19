using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    [Table("TblProject")]
    public class Project : ProjectDTO
    {
        public string ClientId { get; set; }
        public Client? Client { get; set; }
        public Employee? TechnicalProjectManagerId { get; set; }
        public Employee? SalesContactId { get; set; }
        public Employee? PMOId { get; set; }
        public ICollection<SOW> SOW { get; set; }
        public ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
        public ICollection<ProjectTechnology>? ProjectTechnologies { get; set; }
    }
    public class ProjectDTO : AuditData
    {
        public string Client { get; set; }
        public string? ProjectName { get; set; }
        public string[] Technology { get; set; }
        public string? TechnicalProjectManager { get; set; }
        public string? SalesContact { get; set; }
        public string? PMO { get; set; }
        public DateTime? SOWSubmittedDate { get; set; }
        public DateTime? SOWSignedDate { get; set; }
        public DateTime? SOWValidTill { get; set; }
        public DateTime? SOWLastExtendedDate { get; set; }
    }
}
