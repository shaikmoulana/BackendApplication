/*using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Project
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ClientId { get; set; }
        public string ProjectName { get; set; }
        public string TechnicalProjectManager { get; set; }
        public string SalesContact { get; set; }
        public string PMO { get; set; }
        public DateTime SOWSubmittedDate { get; set; }
        public DateTime SOWSignedDate { get; set; }
        public DateTime SOWValidTill { get; set; }
        public DateTime SOWLastExtendedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        [ForeignKey("TechnicalProjectManager")]
        public Employee Employee { get; set; }
        [ForeignKey("SalesContact")]
        public Employee EmployeeSalesContact { get; set; }
        [ForeignKey("PMO")]
        public Employee EmployeesPMO { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployee { get; set; }
    }

    public class ProjectDTO
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string ProjectName { get; set; }
        public string TechnicalProjectManager { get; set; }
        public string SalesContact { get; set; }
        public string PMO { get; set; }
        public DateTime SOWSubmittedDate { get; set; }
        public DateTime SOWSignedDate { get; set; }
        public DateTime SOWValidTill { get; set; }
        public DateTime SOWLastExtendedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set;}
    }
}
*/

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
    public class Project
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ClientId { get; set; }
        public string? ProjectName { get; set; }

        public string? TechnicalProjectManager { get; set; }
        public string? SalesContact { get; set; }
        public string? PMO { get; set; }

        public DateTime? SOWSubmittedDate { get; set; }
        public DateTime? SOWSignedDate { get; set; }
        public DateTime? SOWValidTill { get; set; }
        public DateTime? SOWLastExtendedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Client? Client { get; set; }

        public Employee? TechnicalProjectManagerId { get; set; }
        public Employee? SalesContactId { get; set; }
        public Employee? PMOId { get; set; }

        public ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
    }
    public class ProjectDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ClientId { get; set; }
        public string? ProjectName { get; set; }
        public string? TechnicalProjectManager { get; set; }
        public string? SalesContact { get; set; }
        public string? PMO { get; set; }
        public DateTime? SOWSubmittedDate { get; set; }
        public DateTime? SOWSignedDate { get; set; }
        public DateTime? SOWValidTill { get; set; }
        public DateTime? SOWLastExtendedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
