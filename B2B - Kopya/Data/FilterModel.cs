namespace B2B.Data
{
    public class FilterModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Checked { get; set; }
        public Guid CharValId { get; set; }
        public Guid CharCodeId { get; set; }
    }
}
