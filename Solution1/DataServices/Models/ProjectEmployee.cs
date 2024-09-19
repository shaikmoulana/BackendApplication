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
    public class ProjectEmployee : ProjectEmployeeDTO
    {
        public string? ProjectId { get; set; }
        public string? EmployeeId { get; set; }

        public Project? Project { get; set; }
        public Employee? Employee { get; set; }
    }

    public class ProjectEmployeeDTO : AuditData
    {
        public string? Project { get; set; }
        public string? Employee { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
