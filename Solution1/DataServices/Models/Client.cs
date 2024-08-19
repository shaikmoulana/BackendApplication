using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Client
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string ?LineofBusiness { get; set; }
        public string ?SalesEmployee { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ?UpdatedBy { get; set; }
        public DateTime ?UpdatedDate { get; set; }
        public ICollection<ProjectTechnology> ProjectTechnologies { get; set; }
        public ICollection<ClientContact> ClientContact { get; set; }
        public ICollection<SOW> SOW { get; set; }
        public ICollection<Project> Project { get; set; }
        [ForeignKey("SalesEmployee")]
        public Employee Employee { get; set; }
    }

    public class ClientDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ?LineofBusiness { get; set; }
        public string ?SalesEmployee { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ?UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
