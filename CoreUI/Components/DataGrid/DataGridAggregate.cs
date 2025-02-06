using Microsoft.AspNetCore.Components;

namespace CoreUI.Components.DataGrid
{
    public class DataGridAggregate<TItem>:ComponentBase
    {
        [CascadingParameter] public DataGrid<TItem> Datagrid { get; set; } = default!;
        [Parameter] public string Caption { get; set; } = default!;
        [Parameter] public string Field { get; set; } = default!;
        [Parameter] public TItem Value { get; set; } = default!;
        [Parameter] public TextAlignmet TextAlign { get; set; } = TextAlignmet.Left;

        protected override void OnInitialized()
        {
            if (Datagrid != null)
            {
                Datagrid.AddDataGridAggregates(this);
            }
        }
    }
}
