using System.ComponentModel.DataAnnotations;

namespace CoreUI.Data
{
    public class CartLine
    {

        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public double DisCount {  get; set; }
        public double Tax { get; set; }
        public double DiscountAmount { get {
                return (Price * Amount) * DisCount / 100;
            }
            private set { }
        }
        public double TaxAmount { get {
                return ((Price * Amount) - DisCount) * Tax / 100;
            }
            private set { }
        }
        public double Total { get {
            return ((Price * Amount)-DisCount)+Tax;
            }
            private set { } }
    }
}
