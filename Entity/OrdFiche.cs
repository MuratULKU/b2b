using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class OrdFiche:BaseEntity
    {
        public int LogicalRef { get; set; }
        public string? ClientCode { get; set; }
        public short TrCode { get; set; }
        public string FicheNo { get; set; } = default!;
        public double Total { get; set; }
        public double TotalDiscounted { get; set; }
        public double GrossTotal { get; set; }
        public double TotalVat { get; set; }
        public DateTime Date_ { get; set; }
        public string? Docode { get; set; }
        public byte Send { get; set; }
        public Guid? ApprovingUser { get; set; } 
        public bool Active { get; set; }
        [MaxLength(50)]
        public string? Explanation { get; set; }
        public int CurrencyId { get; set; }
        public Guid UserId { get; set; }
        public List<OrdLine>? Lines { get; set; }
        public User User { get; set; }
        public Currency? Currency { get; set; }
        public Guid CompanyId { get; set; } = Guid.Empty;
        public Company Company { get; set; } = default!;
    }
}
