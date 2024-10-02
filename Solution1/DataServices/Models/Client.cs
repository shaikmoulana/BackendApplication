using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Client : ClientDTO
    {
        public ICollection<ClientContact> ClientContact { get; set; }
        public ICollection<SOW> SOWs { get; set; }
        public ICollection<Project> Project { get; set; }
        public ICollection<POC> POC { get; set; }
        [ForeignKey("SalesEmployee")]
        public Employee Employee { get; set; }
    }

    public class ClientDTO : AuditData
    {
        public string Name { get; set; }
        public string ?LineofBusiness { get; set; }
        public string ?SalesEmployee { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
    }
}
