using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class SOWStatus : SOWStatusDTO
    {
        public ICollection<SOW> SOW { get; set; }

    }
    public class SOWStatusDTO : AuditData
    {
        public string Status { get; set; }

    }
}
