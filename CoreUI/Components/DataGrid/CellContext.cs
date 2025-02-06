namespace CoreUI.Components.DataGrid
{
    public class CellContext<TItem>
    {
        public TItem Item { get; set; }
        public CellContext(DataGrid<TItem> dataGrid, TItem item)
        {
            Item = item;
        }
    }
}
