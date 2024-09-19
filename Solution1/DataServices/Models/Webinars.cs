using DataServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Webinars : WebinarsDTO
    {
        [ForeignKey("Speaker")]
        public Employee Employee { get; set; }
    }
    public class WebinarsDTO : AuditData
    {
        public string? Title { get; set; }
        public string? Speaker { get; set; }
        public string? Status { get; set; }
        public DateTime? WebinarDate { get; set; }
        public int? NumberOfAudience { get; set; }

    }
}
