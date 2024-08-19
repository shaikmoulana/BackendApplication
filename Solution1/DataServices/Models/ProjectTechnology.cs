/*using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class ProjectTechnology
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Project { get; set; }
        public string? Technology { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        [ForeignKey("Project")]
        public Client Client { get; set; }
        [ForeignKey("Technology")]
        public Technology Technologies { get; set; }
    }

    public class ProjectTechnologyDTO
    {
        public string Id { get; set; }
        public string? Project { get; set; }
        public string? Technology { get; set; }
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
    public class ProjectTechnology
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Project { get; set; }
        public string? Technology { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Client? ClientId { get; set; }
        public Technology? TechnologyId { get; set; }
    }
    public class ProjectTechnologyDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Project { get; set; }
        public string? Technology { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

