using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServices.Models;

namespace DataServices.Models
{
    public class Employee : EmployeeDTO
    {
        [StringLength(36)]
        public string? DesignationId { get; set; }
        public string? DepartmentId { get; set; }       
        
        [ForeignKey("DesignationId")]
        public Designation Designation { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        [ForeignKey("Role")]
        public Role Roles { get; set; }

        [ForeignKey("ReportingTo")]
        public Employee? ReportingToEmployee { get; set; }

        public ICollection<Blogs> Blog  { get; set; }
        public ICollection<Client> Client { get; set; }
        public ICollection<Interviews> Interviews { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public ICollection<SOWProposedTeam> SOWProposedTeam { get; set; }
        public ICollection<Webinars> Webinars { get; set; }
        public ICollection<Project> TechnicalProjects{ get; set; }
        public ICollection<Project> SalesProjects { get; set; }
        public ICollection<Project> PMOProjects { get; set; }
        public ICollection<EmployeeTechnology> Technology { get; set; }
        public ICollection<Employee> Subordinates { get; set; }

    }

    public class EmployeeDTO : AuditData
    {

        [Required]
        [StringLength(50)]

        public string Name { get; set; }
        [StringLength(36)]

        public string? Designation { get; set; }
        [Required]

        public string EmployeeID { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string EmailId { get; set; }
        public string? Department { get; set; }
        public string[] Technology {  get; set; } 
        public string? ReportingTo { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? RelievingDate { get; set; }
        public string? Projection { get; set; }
        public string? Password { get; set; }
        public string? Profile { get; set; }
        public string PhoneNo { get; set; }
        public string? Role { get; set; }

    }
}


