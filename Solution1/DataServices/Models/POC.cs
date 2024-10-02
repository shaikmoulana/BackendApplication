using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class POC : POCDTO
    {
        public string? ClientId { get; set; }
        public Client? Client { get; set; }
        public ICollection<POCTeam> POCTeam { get; set; }
        public ICollection<POCTechnology> POCTechnology { get; set; }

    }
    public class POCDTO : AuditData
    {
        public string? Title { get; set; }
        public string? Client { get; set; }
        public string? Status { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string? Document {  get; set; }

    }
}
