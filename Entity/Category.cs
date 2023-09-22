namespace Entity
{
    public class Category : BaseEntity
    {
        public int LogicalRef { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
    }
}
