using Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CoreUI.Components.DataGrid
{

    public partial class DataGrid<TItem>
    {
        private TItem _item;
        public int _rowsperpage = 10;
        private int _currentPage = 0;
        private int _totalCount;
        private string _sortColumn = "";
        private bool _ascending = true;
        private HashSet<TItem> ExpandedRows = new();
        public readonly List<Column<TItem>> RenderedColumns = new List<Column<TItem>>();
        public readonly List<DataGridAggregate<TItem>> DataGridAggregatesColumns = new List<DataGridAggregate<TItem>>();
      
     
        [Parameter] public RenderFragment ButtonRowTemplate { get; set; } = default!;
        [Parameter] public RenderFragment NoDataTemplate { get; set; } = default!;
        [Parameter] public RenderFragment DetailRowTemplate { get; set; } = default!;
        [Parameter] public RenderFragment FooterTemplate { get; set; } =default!;
        [Parameter] public RenderFragment DataGridAggregates { get; set; } = default!;  
        [Parameter] public RenderFragment Columns { get; set; } = default!;
        [Parameter] public RenderFragment<CellContext<TItem>> ChildRowContent { get; set; } = default!;
        [Parameter] public bool Pager { get; set; }

        [Parameter] public bool Virtualize { get; set; }
        [Parameter] public EventCallback<TItem> CommittedItemChanges { get; set; }
        [Parameter] public bool Editable { get; set; } = false;
        [Parameter] public bool ReadOnly { get; set; } = true;
        [Parameter] public string RowClass { get; set; } = default!;
        [Parameter]
        public TItem SelectedItem
        {
            get { return _item; }
            set
            {
                if (!ReferenceEquals(_item, value))
                {
                    _item = value;
                    SelectedItemChanged.InvokeAsync(value);
                }
            }
        }
        [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
        [Parameter] public EventCallback<DataGridRowClickEventArgs<TItem>> RowClick { get; set; } = default!;
        [Parameter] public EventCallback<DataGridHeaderClickEventArgs<TItem>> HeaderClick { get; set; }
        [Parameter] public EventCallback<int> SelectedPageChange { get; set; }
        public Action? PagerStateHasChangedEvent { get; set; }

        [Parameter] public EventCallback<int> RowsPerPage { get; set; }
        public async Task SetRowsPerPageAsync(int size)
        {
            if (_rowsperpage == size)
                return;
            _rowsperpage = size;
            await InvokeAsync(StateHasChanged);
            await RowsPerPage.InvokeAsync(size);

        }
        [Parameter]
        public int CurrentPage
        {
            get
            { return _currentPage + 1; }
            set
            {
                if (_currentPage == value - 1)
                    return;
                _currentPage = value - 1;
                SelectedPageChange.InvokeAsync(value - 1);
            }
        }

        private Func<TItem, object> GetSortKeyFunc(string columnName)
        {
            return item => typeof(TItem).GetProperty(columnName)?.GetValue(item, null);
        }

      
        internal List<TItem> CurrentPageItems 
        {
            get
            {
                var sortedItems = _ascending
                    ? _items.OrderBy(GetSortKeyFunc(_sortColumn)).ToList()
                    : _items.OrderByDescending(GetSortKeyFunc(_sortColumn)).ToList();
                if (Virtualize)
                {
                    if (Pager)
                        return sortedItems
                            .Skip(_currentPage * _rowsperpage)
                            .Take(_rowsperpage)
                            .ToList();
                }

                return sortedItems;
            }
        }
        [Parameter]
        public int TotalCount
        {
            get
            {
                if (_totalCount == null)
                    return _items.Count();
                return _totalCount;
            }
            set
            {
                _totalCount = value;
                InvokeAsync(StateHasChanged);
            }
        }

        [Parameter]
        public int RowCount
        {
            get
            {
              return  _rowsperpage;
            }
            set
            {
                _rowsperpage = value;
            }
        }
        public void NavigateTo(int page)
        {
            CurrentPage = page;
            StateHasChanged();
        }
        internal async Task CommitItemChangesAsync(TItem item)
        {
            await CommittedItemChanges.InvokeAsync(item);
        }

        public async Task OnRowClickedAsync(TItem item, int rowIndex)
        {
            await RowClick.InvokeAsync(new DataGridRowClickEventArgs<TItem>
            {
                Item = item,
                RowIndex = rowIndex
            });
    
            if (!EqualityComparer<TItem>.Default.Equals(SelectedItem, item))
            {
                SelectedItem = item; 
            }
            //await Task.CompletedTask;
            if (DetailRowTemplate != null)
            {
                ToggleDetail(item);
            }
            await InvokeAsync(StateHasChanged);
        }

        private void ToggleDetail(TItem item)
        {
            if (ExpandedRows.Contains(item))
            {
                ExpandedRows.Remove(item);
            }
            else
            {
                ExpandedRows.Add(item);
            }

            StateHasChanged();
        }

        private bool DetailRowVisible(TItem item)
        {
            return ExpandedRows.Contains(item);
        }

        internal async Task OnHeaderColumnClickedAysnc(TItem item, string columnName)
        {

            await HeaderClick.InvokeAsync(new DataGridHeaderClickEventArgs<TItem>
            {
               
                Item = item,
                ColumnName = columnName
            });
            _sortColumn = columnName;
            _ascending = !_ascending;
            await InvokeAsync(StateHasChanged);


        }
    }
}
