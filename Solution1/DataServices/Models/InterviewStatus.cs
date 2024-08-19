using DataServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class InterviewStatus
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Status { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<Interviews> Interviews { get; set; }

    }
    public class InterviewStatusDTO
    {
        public string Id { get; set; }
        public string? Status { get; set; }
        public bool IsActive { get; set; } 
        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; } 
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}



