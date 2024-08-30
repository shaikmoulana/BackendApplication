using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Blogs
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [StringLength(200)]
        public string? Title { get; set; }
        public string? Author { get; set; }
        [StringLength(50)]
        public string? Status { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsActive { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [StringLength(50)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [ForeignKey("Author")]
        public Employee? Employee { get; set; }
    }

    public class BlogsDTO {
        public string Id { get; set; } 
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Status { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } 
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }


    }

}

