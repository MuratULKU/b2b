namespace Entity
{
    public class FirmParam : BaseEntity
    {
        public int FirmId { get; set; } = 1;
        public int SyncMinute { get; set; } = 60;
        public DateTime? LastSync { get; set; }
        public string? FirmName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Town { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? MailAddress { get; set; }


    }
}
