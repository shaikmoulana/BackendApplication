using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Designation : DesignationDTO
    {
        
        public ICollection<Employee> Employee { get; set; }
        public ICollection<SOWRequirement> SOWRequirement { get; set; }

    }

    public class DesignationDTO : AuditData
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
