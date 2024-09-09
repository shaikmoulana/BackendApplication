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
    public class Employee
    {

        [Key]
        [Required]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(36)]
        public string? DesignationId { get; set; }

        [Required]
        public string EmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string EmailId { get; set; }
        public string? DepartmentId { get; set; }
        public string? ReportingTo { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? RelievingDate { get; set; }
        public DateTime? Projection { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string? Password { get; set; }
        public string ? Profile {  get; set; }
        public string PhoneNo { get; set; }
        
        [ForeignKey("DesignationId")]
        public Designation Designation { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public ICollection<Blogs> Blog  { get; set; }
        public ICollection<Client> Client { get; set; }
        public ICollection<Interviews> Interviews { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public ICollection<SOWProposedTeam> SOWProposedTeam { get; set; }
        public ICollection<Webinars> Webinars { get; set; }
        public ICollection<Project> TechnicalProjectManagerId { get; set; }
        public ICollection<Project> SalesContactId { get; set; }
        public ICollection<Project> PMOId { get; set; }
        public ICollection<EmployeeTechnology> EmployeeTechnology { get; set; }

    }

    public class EmployeeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Designation { get; set; }
        public string EmployeeID { get; set; }
        public string EmailId { get; set; }
        public string? Department { get; set; }
        public string? ReportingTo { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? RelievingDate { get; set; }
        public DateTime? Projection { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string? Password { get; set; }
        public string? Profile { get; set; }
        public string PhoneNo { get; set; }

    }
}


