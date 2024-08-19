/*using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class ProjectEmployee
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Project { get; set; }
        public string? Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("Project")]
        public Project Projects { get; set; }
        [ForeignKey("Employee")]
        public Employee Employees { get; set; }
    }
    public class ProjectEmployeeDTO
    {
        public string Id { get; set; }
        public string? Project { get; set; }
        public string? Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

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
    [Table("TblProjectEmployee")]
    public class ProjectEmployee
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Project { get; set; }
        public string? Employee { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Project? ProjectId { get; set; }
        public Employee? EmployeeId { get; set; }
    }

    public class ProjectEmployeeDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Project { get; set; }
        public string? Employee { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
