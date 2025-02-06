namespace CoreUI.Components.DataGrid
{
    public class Cell<TItem>
    {
        private readonly DataGrid<TItem> _dataGrid;
        private readonly Column<TItem> _column;
        internal TItem _item;
        internal object Value;

        internal bool isEditing;
        internal CellContext<TItem> cellContext;
        public Cell(DataGrid<TItem> dataGrid, Column<TItem> column, TItem item)
        {
            _dataGrid = dataGrid;
            _column = column;
            _item = item;
            OnStartedEditingItem();
            cellContext = new CellContext<TItem>(_dataGrid, _item);
        }



        private void OnStartedEditingItem()
        {
            var property = _item.GetType().GetProperties().SingleOrDefault(x => x.Name == _column.Field);
            if (property != null)
                Value = property.GetValue(_item);
        }

        public async Task StringValueChangedAsync(object value)
        {
            var property = _item.GetType().GetProperties().SingleOrDefault(x => x.Name == _column.Field);
            property.SetValue(_item, value);
            await _dataGrid.CommitItemChangesAsync(_item);
        }

        public async Task NumberValueChangedAsync(object value)
        {
            var property = _item.GetType().GetProperties().SingleOrDefault(x => x.Name == _column.Field);
            property.SetValue(_item, value);
            await _dataGrid.CommitItemChangesAsync(_item);
        }
    }
}
