using DataServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class InterviewStatus : InterviewStatusDTO
    {
        public ICollection<Interviews> Interviews { get; set; }

    }
    public class InterviewStatusDTO : AuditData
    {
        public string? Status { get; set; }
    }
}



