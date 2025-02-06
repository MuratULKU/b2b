using Microsoft.AspNetCore.Components.Web;

namespace CoreUI.Components.DataGrid
{
    public class DataGridRowClickEventArgs<TItem>:EventArgs
    {
        public MouseEventArgs MouseEventArgs { get; set; } = default!;
        public TItem Item { get; set; }= default!;
        public int RowIndex { get; set; }
    }
}
