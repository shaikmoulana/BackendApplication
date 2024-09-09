using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class EmployeeTechnology
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string EmployeeID { get; set; }
        public string? Technology { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("Technology")]
        public Technology Technologies { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
    public class EmployeeTechnologyDTO
    {
        public string Id { get; set; }
        public string EmployeeID { get; set; }
        public string? Technology { get; set; }
        public bool IsActive { get; set; } 
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } 
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
