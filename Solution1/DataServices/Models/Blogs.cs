using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Blogs : BlogsDTO
    {
        public Employee? Employee { get; set; }
    }

    public class BlogsDTO : AuditData
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Status { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
     
    }

}

