namespace B2B.Data
{
    public class ClientFiche
    {
        public DateTime Date { get; set; }
        public string TranNo { get; set; }
        public string DocNo { get; set; }
        public short TrCode { get; set; }
        public String TrString { get; set; }
        public int LogicalRef { get; set; }
        public short ModulNr { get; set; }
        public short Sing { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Amount { get; set; }
        public int SourceRef { get; set; }
        public double Balance { get; set; }
        public string LineExp { get; set; }
        public List<Invoice> GetInvoice { get; set; }
    }
}
