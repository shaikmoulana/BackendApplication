using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class EmployeeTechnology : EmployeeTechnologyDTO
    {
     
        [ForeignKey("Technology")]
        public Technology Technologies { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
    public class EmployeeTechnologyDTO : AuditData
    {
        public string EmployeeID { get; set; }
        public string? Technology { get; set; }
     
    }
}
