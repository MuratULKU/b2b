using Core.Concrete;
using Microsoft.AspNetCore.Components;

namespace CoreUI.Components.DataGrid
{
    public partial class DataGridPager<TItem> : ComponentBase, IDisposable
    {
        [CascadingParameter] public DataGrid<TItem> Datagrid { get; set; }


        private int PageCount;
        protected int StartIndex { get; private set; } = 1;
        protected int FinishIndex { get; private set; } = 3;

        protected int[] PageSizeSelectorItems = new int[] { 5, 10, 20, 50 };
        
        private bool HasPreviousPage => Datagrid.CurrentPage > 1;
        private bool HasNextPage => Datagrid.CurrentPage  < PageCount;

        protected override void OnParametersSet()
        {
            PageCount = (int)Math.Ceiling((double)Datagrid.TotalCount / (double)Datagrid.RowCount);
            int visiblePages = Math.Min(5, PageCount);

            StartIndex = (int)Math.Max(1, Math.Ceiling((decimal)(Datagrid.CurrentPage - (visiblePages / 2))));
            FinishIndex = Math.Min(PageCount, StartIndex + visiblePages - 1);

            var delta = 5 - (FinishIndex - StartIndex + 1);
            StartIndex = Math.Max(1, StartIndex - delta);
           
            base.OnParametersSet();
        }

        protected override async Task OnInitializedAsync()
        {
            if (Datagrid != null)
            {
                Datagrid.PagerStateHasChangedEvent += StateHasChanged;
                await Datagrid.SetRowsPerPageAsync(Datagrid._rowsperpage);
            }
        }

       

        private void PageOnChanged(int pageNumber) 
        {
            Datagrid.NavigateTo(pageNumber);
            OnParametersSet();
            StateHasChanged();
        }
        private async void PerRowPageOnChange(ChangeEventArgs e)
        {
            if (Datagrid != null)
            {
                PageCount = (int)Math.Ceiling((double)Datagrid.TotalCount / Convert.ToDouble(e.Value));
               
                await Datagrid.SetRowsPerPageAsync(Convert.ToInt32(e.Value));
                 PageOnChanged(1);
            }
        }

        public void Dispose()
        {
            if (Datagrid != null)
                Datagrid.PagerStateHasChangedEvent -= StateHasChanged;
        }
    }
}
