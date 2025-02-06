using CoreUI.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CoreUI.Components.Pagenation
{
    public class PagerModel : UlkuComponentBase
    {
        // PagedResultBase, sayfalama için veri içeriyor.
        [Parameter]
        public PagedResultBase Result { get; set; }

        // Sayfa değiştiğinde tetiklenen event
        [Parameter]
        public Action<int> PageChanged { get; set; }

        // Satır sayısını tutar ve pageCount hesaplamasında kullanılır.
        private int _rowCount;
        [Parameter]
        public int RowCount
        {
            get { return _rowCount; }
            set
            {
                _rowCount = value;
                Result.PageCount = (int)Math.Ceiling((double)_rowCount / _pageSize);
            }
        }

        // Sayfa başına gösterilecek öğe sayısını tutar.
        private int _pageSize;
        [Parameter]
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                // Sayfa boyutu değiştiğinde sayfa sayısını güncelle
                Result.PageCount = (int)Math.Ceiling((double)_rowCount / _pageSize);
            }
        }

        // Sayfa aralığının başlangıcı ve bitişi
        protected int StartIndex { get; private set; } = 0;
        protected int FinishIndex { get; private set; } = 0;

        // Parametreler her set edildiğinde sayfalama ayarlarını güncelle
        protected override void OnParametersSet()
        {
            // Gösterilecek sayfa sayısı, 5'e kadar görünür
            int visiblePages = Math.Min(5, Result.PageCount);

            // Sayfa numarasını ortalamaya göre hesapla
            StartIndex = Math.Max(1, Result.CurrentPage - visiblePages / 2);
            FinishIndex = Math.Min(Result.PageCount, StartIndex + visiblePages - 1);

            // Kalan sayfa sayısına göre başlangıç indeksini ayarla
            var delta = 5 - (FinishIndex - StartIndex + 1);
            StartIndex = Math.Max(1, StartIndex - delta);

            base.OnParametersSet();
        }

        // Sayfa düğmesine tıklandığında çağrılır
        protected void PagerButtonClicked(int page)
        {
            Result.CurrentPage = page;
            PageChanged?.Invoke(page);
        }

        // Sayfa boyutu seçim listesi
        protected int[] PageSizeSelectorItems = new int[] { 5, 10, 20, 50 };

        // Sayfa boyutu değiştiğinde tetiklenir
        [Parameter] public EventCallback<int> OnPageSizeChanged { get; set; }
        protected void PageSizeChanged(ChangeEventArgs args)
        {
            if (int.TryParse(args?.Value?.ToString(), out var newPageSize))
            {
                OnPageSizeChanged.InvokeAsync(newPageSize);
            }
        }
    }
}
