namespace B2C.Data
{
    public class PaymentViewModel
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public string CvvCode { get; set; }
        public string CardType { get; set; }
        public int Installment { get; set; }
        public bool ManufacturerCard { get; set; }
        public decimal TotalAmount { get; set; }
        public string Explanation { get; set; }
        public Guid VirtualPosId { get; set; }
        public string ClientCode { get; set;}
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
