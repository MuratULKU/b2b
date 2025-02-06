using Microsoft.AspNetCore.Components;

namespace B2B.Components.Pagination
{
    public class PagerModel : ComponentBase
    {
        [Parameter]
        public PagedResultBase Result { get; set; }

        [Parameter]
        public Action<int> PageChanged { get; set; }

        private int _rowCount;
        [Parameter]
        public int RowCount
        {
            get { return _rowCount; }
            set
            {
                _rowCount = value;
                Result.PageCount = (int)Math.Ceiling((double)_rowCount / (double)_pageSize);
            }
        }
        private int _pageSize;
        [Parameter]
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
            }
        }
        protected int StartIndex { get; private set; } = 0;
        protected int FinishIndex { get; private set; } = 0;

        protected override void OnParametersSet()
        {
            StartIndex = Math.Max(Result.CurrentPage - 2, 1);
            FinishIndex = Math.Min(Result.CurrentPage + 4, Result.PageCount);
           if(Result.CurrentPage  == 0)
            {
                Result.CurrentPage = 1;
            }
            if (Result.PageCount < 5)
            {
                FinishIndex = Result.PageCount;
                StartIndex = 1;
            }
            else if (FinishIndex < 5 && Result.PageCount > 5)
                FinishIndex = 5;

            if (FinishIndex - 5 != StartIndex && StartIndex > 2)
                StartIndex = FinishIndex - 4;
            else if (Result.CurrentPage > Result.PageCount - 5 && StartIndex > 2)
            {
                StartIndex = Result.PageCount - 4;
            }
            base.OnParametersSet();
        }

        protected void PagerButtonClicked(int page)
        {
            Result.CurrentPage = page;
            PageChanged?.Invoke(page);
        }

        //gösterilecek eleman dsayısını ayarlama
        protected int[] PageSizeSelectorItems = new int[] { 5, 10, 20, 50 };
        [Parameter] public EventCallback<int> OnPageSizeChanged { get; set; }
        protected void PageSizeChanged(ChangeEventArgs args)
        {
            int.TryParse(args?.Value?.ToString(), out var newPageSize);
            OnPageSizeChanged.InvokeAsync(newPageSize);
        }
    }
}
