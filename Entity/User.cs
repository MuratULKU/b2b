namespace Entity
{
    public class User : BaseEntity
    {
        public Guid? CompanyId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public double? Discount { get; set; } = 0;
        public string? AccountCode { get; set; } //pss de plasiyer kodu olarak kullanılacak
        public string? AccountName { get; set; } //pss plasiyer adı ve soyadı olarak kullanılacak
        public bool? Active { get; set; } = false;
        public ICollection<UserRole>? UsersRoles { get; set; }
      
    }
}