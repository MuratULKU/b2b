using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
        public virtual DateTime CreateDate { get; set; } 
        public virtual DateTime UpdateDate { get; set; } = DateTime.Now;
        public virtual Guid CreateUser { get; set; }
        public virtual Guid UpdateUser { get; set; }
    }
}
