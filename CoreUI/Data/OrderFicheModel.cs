using CoreUI.Data;
using Entity;

namespace CoreUI.Data
{
    public class OrderFicheModel:BaseEntity
    {
        public List<OrderLineModel> Lines { get; set; }
    }
}
