namespace Entity
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
