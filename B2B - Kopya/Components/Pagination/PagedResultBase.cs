namespace B2B.Components.Pagination
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; } = 0;
        public int PageCount { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public int RowCount { get; set; } = 0;

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < PageCount;
        public int FirstRowOnPage
        {
            get {return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }
}
