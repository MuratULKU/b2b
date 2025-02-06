using Entity;

namespace CoreUI.Data
{
    public class OrderLineModel:BaseEntity
    {
        public Product Product { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }

    }
}
