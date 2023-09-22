namespace Entity
{
    public class Product : BaseEntity
    {
        public int LogicalRef { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentRef { get; set; }
        public double? Vat { get; set; }
        public double? SellVat { get; set; }
        public string? StGrupCode { get; set; }
        public string? ProducerCode { get; set; }
        public string? SpeCode { get; set; }
        public string? SpeCode2 { get; set; }
        public string? SpeCode3 { get; set; }
        public string? SpeCode4 { get; set; }
        public string? SpeCode5 { get; set; }
        public string Unit { get; set; }
        public string? Unit1 { get; set; }
        public decimal? Unit1rate { get; set; }
        public string? Unit2 { get; set; }
        public decimal? Unit2rate { get; set; }
        public string? Unit3 { get; set; }
        public decimal? Unit3rate { get; set; }
    }
}
