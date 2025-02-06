using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class OrdLine : BaseEntity
    {
        public int Logicalref { get; set; }
        public int StockRef { get; set; }
        
        public int ClientRef { get; set; }
        public short LineType { get; set; }
        public short LineNo { get; set; }
        public short TrCode { get; set; }
        public DateTime Date_ { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }

        public double Total { get; set; }
        public double Discper { get; set; }
        public double Distdisc { get; set; }
        public double Vat { get; set; }
        public double VatAmnt { get; set; }
        public double VatMatrah { get; set; }
        public double UomRef { get; set; }
        public double UsRef { get; set; }
        public string? Unit { get; set; }
       
        public double AvailableStock { get; set; }

        public Guid ProductId { get; set; }
      
        public Product? Product { get; set; }

        public Guid OrdFicheId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        [NotMapped]
        public string? Name { get; set; }

        [NotMapped]
        public string? Code { get; set; }
    }
}
