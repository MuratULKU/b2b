using Microsoft.AspNetCore.Components;


namespace CoreUI.Components.DataGrid
{
    public enum TextAlignmet
    {
        Left,
        Right,
        Center
    }

    public enum SortDirection
    {
        Asc,
        Desc
    }
    public class Column<TItem> :ComponentBase
    {
        [CascadingParameter] public DataGrid<TItem> Datagrid { get; set; } = default!;
        [Parameter] public string Caption { get; set; } = default!;
        [Parameter] public string Title { get; set; } = default!;
        [Parameter] public TItem Value { get; set; } = default!;
        [Parameter] public string Field { get; set; }=default!;
        [Parameter] public Type FieldType { get; set; } = default!;
        [Parameter] public RenderFragment ChildContent { get; set; }=default!;
        [Parameter] public int Width { get; set; }
        [Parameter] public bool IsEditable { get; set; }
        [Parameter] public bool IsSortable { get; set; }
        [Parameter] public SortDirection SortDirection { get; set; } = SortDirection.Asc;
        [Parameter] public RenderFragment<CellContext<TItem>> CellTemplate { get; set; }
        [Parameter] public string CssStyle { get; set; }
        [Parameter] public TextAlignmet TextAlign { get; set; } = TextAlignmet.Left;
        [Parameter] public Dictionary<string, object> Attributes { get; set; } = new(); 
        protected override void OnInitialized()
        {
            if (null != Datagrid)
                Datagrid.AddColumn(this);
            switch (TextAlign)
            {
                case TextAlignmet.Left:
                    CssStyle = new CssBuilder().AddClass("text-align: left").Build();
                   
                    break;
                case TextAlignmet.Right:
                    CssStyle = new CssBuilder().AddClass("text-align: right;padding-right: 10px").Build();
                 
                    break;
                case TextAlignmet.Center:
                    CssStyle = new CssBuilder().AddClass("text-align: center").Build();
                    break;
                default:
                    break;
            }
            if (Width > 0)
            {
                if (CssStyle != null)
                    CssStyle += new CssBuilder().AddClass($";width:{Width}px").Build();
                else
                    CssStyle = new CssBuilder().AddClass($"width:{Width}px").Build();
            }

            if (IsSortable)
            {
                if (CssStyle != null)
                    CssStyle += new CssBuilder().AddClass($";cursor:pointer").Build();
                else
                    CssStyle = new CssBuilder().AddClass($"cursor:pointer").Build();
            }
           
       }

        internal Type DataType
        {
            get
            {
                if (FieldType != null)
                    return FieldType;
                if (Field == null)
                    return typeof(object);
                if (typeof(TItem) == typeof(IDictionary<string, object>) && FieldType == null)
                    throw new ArgumentNullException(nameof(FieldType));
                var type = typeof(TItem).GetProperty(Field).PropertyType;
                return Nullable.GetUnderlyingType(type) ?? type;
            }
        }
    }
}
