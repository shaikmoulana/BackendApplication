using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class ClientContact
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string ClientId { get; set; }

        public string? ContactValue { get; set; }

        public int? ContactType { get; set; }

        public bool IsActive { get; set; } = true;

        public string CreatedBy { get; set; } = "SYSTEM";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
    public class ClientContactDTO
    {
        public string Id { get; set; }
        public string Client { get; set; }

        public string? ContactValue { get; set; }

        public int? ContactType { get; set; }

        public bool IsActive { get; set; } = true;

        public string CreatedBy { get; set; } = "SYSTEM";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}
