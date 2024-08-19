using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Webinars
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Title { get; set; }
        public string? Speaker { get; set; }
        public string? Status { get; set; }
        public DateTime? WebinarDate { get; set; }
        public int? NumberOfAudience { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [ForeignKey("Speaker")]
        public Employee Employee { get; set; }
    }
    public class WebinarsDTO
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Speaker { get; set; }
        public string? Status { get; set; }
        public DateTime? WebinarDate { get; set; }
        public int? NumberOfAudience { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get;set; }

    }
}
