using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Product : BaseEntity
    {
        public int LogicalRef { get; set; }
        public Int16 Active { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public string? Name3 { get; set; }
        public string? Name4 { get; set; }
        public int? ParentRef { get; set; }
        public double Vat { get; set; }
        public double? SellVat { get; set; }
        public string? StGrupCode { get; set; }
        public string? ProducerCode { get; set; }
        public string? SpeCode { get; set; }
        public string? SpeCode2 { get; set; }
        public string? SpeCode3 { get; set; }
        public string? SpeCode4 { get; set; }
        public string? SpeCode5 { get; set; }
        public string? Keyword1 { get; set; }
        public string? Keyword2 { get; set; }
        public string? Keyword3 { get; set; }
        public string? Keyword4 { get; set; }
        public string? Keyword5 { get; set; }
        public string? Unit { get; set; }
        public string? Unit1 { get; set; }
        public decimal? Unit1rate { get; set; }
        public string? Unit2 { get; set; }
        public decimal? Unit2rate { get; set; }
        public string? Unit3 { get; set; }
        public decimal? Unit3rate { get; set; }
        public int UsRef { get; set; }
        public int UomRef { get; set; }
        public Guid? CharSetId { get; set; }
        public List<ProductAmount>? ProductAmounts { get; set; }
        public List<PriceList>? PriceLists { get; set; }
        public List<FirmDoc>? firmDocs { get; set; }
        public List<CharAsgn>? CharAsgn { get; set; }
        public CharSet? CharSet { get; set; }  

        [NotMapped]
        public double OrderAmount { get; set; } = 1;
        [NotMapped]
        public string CharSetCode { get; set; } = string.Empty;


      
    }
}
