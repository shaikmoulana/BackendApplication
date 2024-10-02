using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class POCTechnology : POCTechnologyDTO
    {
        public string? POCId { get; set; }
        public string TechnologyId { get; set; }
        public POC? POC { get; set; }
        public Technology? Technology { get; set; }

    }
    public class POCTechnologyDTO : AuditData
    {
        public string? POC { get; set; }
        public string? Technology { get; set; }

    }
}
