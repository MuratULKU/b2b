
using Microsoft.AspNetCore.Components;

namespace CoreUI.Components.DataGrid
{
    public partial class MVirtualize<TItem>:ComponentBase
    {
        [Parameter] public bool IsEnabled { get; set; }
        [Parameter] public RenderFragment<TItem> ChildContent { get; set; }
        [Parameter] public ICollection<TItem> Items { get; set; }
    }
}
