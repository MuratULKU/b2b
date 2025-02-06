

using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace CoreUI.BackOrder
{
    public static class BackOrderExtensions
    {
        public static IServiceCollection AddBackOrederServices(this IServiceCollection services)
        {
            services.AddSingleton<IBackOrderProductService, BackOrderProductService>();
            services.AddSingleton<IBackOrderCategoryService, BackOrderCategoryService>();
            services.AddSingleton<IBackOrderPriceListService, BackOrderPriceListService>();
            services.AddSingleton<IBackOrderProductAmountService, BackOrderProductAmountService>();
            services.AddSingleton<IBackOrderClientService, BackOrderClientService>();
            services.AddScoped<IFirmDocRepository,FirmDocRepository>();
            services.AddScoped<ICharSetService,CharSetManager>();
            services.AddScoped<ICharAsgnService, CharAsgnManager>();
            services.AddScoped<ICharCodeService, CharCodeManager>();
            services.AddScoped<IPriceListService, PriceListManager>();
            services.AddScoped<IProductAmountService, ProductAmountManager>();
            //services.AddSingleton<IBackOrderCharSets, BackOrderCharSets>();
            services.AddSingleton<IBackOrderOder, BackOrderOrder>();
            return services;
        }
    }
}
