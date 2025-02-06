using Business.Abstract;
using CoreUI.Components.Base;
using CoreUI.Data;
using Entity;
using Microsoft.AspNetCore.Components;



namespace B2C.Components.Products
{
    public partial class ProductMainPage
    {

        protected CoreUI.Components.Pagenation.PagedResult<Product> pdata = new();
        public Dictionary<Guid, List<string>> PropertySet { get; set; } = default!;
       
        private MenuItemModel selectCategory { get; set; } = default!;
        private List<MenuItemModel> menuItems { get; set; } = default!; 
        public string? Filter { get; set; } = default!;

        public List<Product> ProductLlist { get; set; } = default!;
        [Inject] IProductServices ProductServices { get; set; }=default!;
        [Inject] ICategoryService categoryManager { get; set; } =default!;

        [Parameter]
        [SupplyParameterFromQuery(Name ="q")]
        public string q { get; set; }= default!;   
       

        protected override void OnInitialized()
        {
            Filter = q;
            base.OnInitialized();
        }

       

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                PagerPageChanged(pdata.CurrentPage);
                
            }
        }
        protected async void PagerPageChanged(int page)
        {

            pdata.RowCount = await ProductServices.TotalCount(Filter ?? string.Empty, PropertySet, selectCategory is null ? 0 : selectCategory.id, pdata.CurrentPage, pdata.PageSize);
            pdata.CurrentPage = page < 1 ? 1 : page;
            pdata.Results = await ProductServices.GetAllAsync(Filter ?? string.Empty, PropertySet, selectCategory is null ? 0 : selectCategory.id, pdata.CurrentPage, pdata.PageSize);
           
          

        }
        protected async void pageSizeOnchanged(int pageSize)
        {
            pdata.PageSize = pageSize;
            pdata.CurrentPage = 1;
            pdata.Results = await ProductServices.GetAllAsync(Filter ?? string.Empty, null, selectCategory is null ? 0 : selectCategory.id, pdata.CurrentPage, pdata.PageSize);
            
        }

        private async void changedCategory(MenuItemModel model)
        {
            if(model != null)
            {
                pdata.Results = await ProductServices.GetAllAsync(Filter ?? string.Empty, null,  model.id, pdata.CurrentPage, pdata.PageSize);
                pdata.RowCount = await ProductServices.TotalCount(Filter ?? string.Empty, null, model.id, pdata.CurrentPage, pdata.PageSize);
            }
        }

    }
}
