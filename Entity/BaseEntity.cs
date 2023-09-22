using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public Guid CreateUser { get; set; }
        public Guid UpdateUser { get; set; }
    }
}
