namespace PSS.Data
{
    public class UserSession
    {
        public string UserName { get; set; }
        public string[] Role { get; set; }
        public Guid UserId { get; set; }
    }
 
}
