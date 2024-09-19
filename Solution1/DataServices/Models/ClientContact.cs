using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class ClientContact : ClientContactDTO
    {
        public string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
    public class ClientContactDTO : AuditData
    {
        public string Client { get; set; }

        public string? ContactValue { get; set; }

        public int? ContactType { get; set; }

    }
}
