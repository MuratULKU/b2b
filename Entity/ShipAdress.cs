using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ShipAdress : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string SpeCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string Inchange { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
