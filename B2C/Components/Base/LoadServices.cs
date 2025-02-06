using _3DPayment;


using Business.Abstract;
using Business.Concrete;
using CoreUI.Components.Base;
using B2C.Components.CartPanel;
using CoreUI.Components.Utilities;
using DataAccess.Abstract;
using DataAccess.Concrete;
using SanalMagaza.Business.Concrete;
using B2C.Components.UserPanel;


namespace B2C.Components.Base
{
    public static class LoadServices
    {
        public static IServiceCollection AddUtiliesService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<BootstrapClassProvider>();
            serviceCollection.AddScoped<SessionManager>();
            serviceCollection.AddScoped<CartService>();
            serviceCollection.AddScoped<IIdGenerator, IdGenerator>();
            serviceCollection.AddScoped<IUserIdentityProcessor,UserIdentityProcessor>();
            return serviceCollection;
        }

        public static IServiceCollection AddRepositoryService(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<IPriceListRepository, PriceListRepository>();
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddScoped<IOrdFicheRepository, OrdFicheRepository>();
            serviceCollection.AddScoped<IFirmParamRepository, FirmParamRepository>();
            serviceCollection.AddScoped<IFirmDocRepository, FirmDocRepository>();
            serviceCollection.AddScoped<ICharSetRepository, CharSetRepository>();
            serviceCollection.AddScoped<IUserService, UserManager>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IUserRoleRepository, UserRoleRepository>();
            serviceCollection.AddScoped<IRoleRepository, RoleRepository>();
            serviceCollection.AddScoped<IRoleService, RoleManager>();
            serviceCollection.AddScoped<FirmParamService, FirmParamManager>();
            serviceCollection.AddScoped<IPaymentRepository, PaymentRepository>();
            serviceCollection.AddScoped<IClientRepository, ClientRepository>();
            serviceCollection.AddScoped<IDocumentNoRepository, DocumentNoRepository>();
            return serviceCollection;
        }
        public static IServiceCollection AddBusinessService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBankCardService, BankCardManager>();
            serviceCollection.AddScoped<ICardBrandService, CardBrandManager>();
            serviceCollection.AddScoped<IProductServices, ProductManager>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IOrderService, OrderManager>();
            serviceCollection.AddScoped<IUserService, UserManager>();
            serviceCollection.AddScoped<IUserRoleService, UserRoleManager>();
            serviceCollection.AddScoped<IVirtualPosService, VirtualPosManager>();
            serviceCollection.AddScoped<IVirtualPosParameterService, VirtualPosParameterManager>();
            serviceCollection.AddScoped<ICreditCardService, CreditCardManager>();
            serviceCollection.AddScoped<ICreditCardPrefixService, CreditCardPrefixManager>();
            serviceCollection.AddScoped<ICreditCardInstallmentService, CreditCardInstallmentManager>();
            serviceCollection.AddScoped<IOrderService, OrderManager>();
            serviceCollection.AddScoped<IPaymentService, PaymentManager>();
            serviceCollection.AddScoped<IPaymentProviderFactory, PaymentProviderFactory>();
            serviceCollection.AddScoped<ICharSetService, CharSetManager>();
            serviceCollection.AddScoped<ICharAsgnService, CharAsgnManager>();
            serviceCollection.AddScoped<ICharCodeService, CharCodeManager>();
            serviceCollection.AddScoped<ICharValService, CharValManager>();
            serviceCollection.AddScoped<IProductAmountRepository, ProductAmountRepository>();
            serviceCollection.AddScoped<IPriceListService, PriceListManager>();
            serviceCollection.AddScoped<IProductAmountService, ProductAmountManager>();
            serviceCollection.AddScoped<IClientCardService, ClientCardManager>();
            serviceCollection.AddScoped<IDocumentNoService, DocumentNoManager>();
            return serviceCollection;
        }
    }
}
