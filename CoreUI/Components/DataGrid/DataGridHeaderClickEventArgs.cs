using Microsoft.AspNetCore.Components.Web;

namespace CoreUI.Components.DataGrid
{
    public class DataGridHeaderClickEventArgs<TItem>:EventArgs
    {
        public MouseEventArgs MouseEventArgs { get; set; } = default!;
        public TItem Item { get; set; }
        public string ColumnName { get; set; }
    }
}
