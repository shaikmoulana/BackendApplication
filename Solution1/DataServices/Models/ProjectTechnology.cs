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
    public class ProjectTechnology : ProjectTechnologyDTO
    {
        public string? ProjectId { get; set; }
        public string? TechnologyId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        [ForeignKey("TechnologyId")]
        public Technology Technologies { get; set; }
    }
    public class ProjectTechnologyDTO : AuditData
    {
        public string? Project { get; set; }
        public string? Technology { get; set; }
    }
}

