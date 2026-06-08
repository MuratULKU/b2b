

using Business.Abstract;
using Business.Concrete;
using CoreUI.BackOrder.Ulku;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace CoreUI.BackOrder
{
    public static class BackOrderExtensions
    {
        public static IServiceCollection AddBackOrederServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IBackOrderProductService, BackOrderProductService>(client =>
                client.BaseAddress = new Uri(configuration["ApiService:Url"]));
            services.AddSingleton<IBackOrderCategoryService, BackOrderCategoryService>();
            services.AddSingleton<IBackOrderPriceListService, BackOrderPriceListService>();
            services.AddSingleton<IBackOrderProductAmountService, BackOrderProductAmountService>();
            services.AddHttpClient<IBackOrderClientService, BackOrderClientService>(client =>
                client.BaseAddress = new Uri(configuration["ApiService:Url"]));
            services.AddScoped<IFirmDocRepository,FirmDocRepository>();
            services.AddScoped<ICharSetService,CharSetManager>();
            services.AddScoped<ICharAsgnService, CharAsgnManager>();
            services.AddScoped<ICharCodeService, CharCodeManager>();
            services.AddScoped<IPriceListService, PriceListManager>();
            services.AddScoped<ICharValService, CharValManager>();
            services.AddScoped<IProductAmountService, ProductAmountManager>();
            //services.AddSingleton<IBackOrderCharSets, BackOrderCharSets>();
            services.AddHttpClient<IBackOrderOder, BackOrderOrder>(client =>
                client.BaseAddress = new Uri(configuration["ApiService:Url"]));
            return services;
        }
    }
}
