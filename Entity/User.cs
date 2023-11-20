namespace Entity
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AccountCode { get; set; }
        public ICollection<UserRole> UsersRoles { get; set; }

    }
}