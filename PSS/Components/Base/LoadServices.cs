

using Business.Abstract;
using Business.Concrete;
using Core.Logger;
using CoreUI.Components.Base;
using CoreUI.Components.Confirm;
using CoreUI.Components.Utilities;
using DataAccess.Abstract;
using DataAccess.Concrete;
using SanalMagaza.Business.Concrete;




namespace B2C.Components.Base
{
    public static class LoadServices
    {
        public static IServiceCollection AddUtiliesService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<BootstrapClassProvider>();
            serviceCollection.AddScoped<SessionManager>();
            serviceCollection.AddScoped<IIdGenerator, IdGenerator>();
           
            return serviceCollection;
        }

        public static IServiceCollection AddRepositoryService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPaymentRepository, PaymentRepository>();
            serviceCollection.AddScoped<IOrdFicheRepository, OrdFicheRepository>();
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            return serviceCollection;
        }
        public static IServiceCollection AddBusinessService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRoleService, RoleManager>();
            serviceCollection.AddScoped<IUserService,UserManager>();
            serviceCollection.AddScoped<IUserRoleService, UserRoleManager>();
            serviceCollection.AddScoped<IClientCardService,ClientCardManager>();
            serviceCollection.AddScoped<IProductService,ProductManager>();
            serviceCollection.AddScoped<ICategoryService,CategoryManager>();
            serviceCollection.AddScoped<IOrderService,OrderManager>();
            serviceCollection.AddScoped<IFirmParamService,FirmParamManager>();
            serviceCollection.AddScoped<IDocumentNoService,DocumentNoManager>();
            serviceCollection.AddScoped<ICompanyService, CompanyManager>();
            serviceCollection.AddScoped<ICurrencyService, CurrencyManager>();
            serviceCollection.AddScoped<IVirtualPosService, VirtualPosManager>();
            serviceCollection.AddScoped<ICreditCardPrefixService, CreditCardPrefixManager>();
            serviceCollection.AddScoped<ICreditCardService, CreditCardManager>();
            serviceCollection.AddScoped<ICardBrandService, CardBrandManager>();
            serviceCollection.AddScoped<IClFicheService,ClFicheManager>();
            serviceCollection.AddScoped<IBankCardService, BankCardManager>();
            serviceCollection.AddScoped<IVatListService, VatListManager>();
            return serviceCollection;
        }
    }
}
