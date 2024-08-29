using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Designation
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public ICollection<Employee> Employee { get; set; }
        public ICollection<SOWRequirement> SOWRequirement { get; set; }

    }

    public class DesignationDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
