

using Business.Abstract;
using Business.Concrete;
using CoreUI.Components.Base;
using CoreUI.Components.Utilities;
using DataAccess.Abstract;
using DataAccess.Concrete;
using PSS.Components.CartPanel;



namespace B2C.Components.Base
{
    public static class LoadServices
    {
        public static IServiceCollection AddUtiliesService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<BootstrapClassProvider>();
            serviceCollection.AddScoped<SessionManager>();
            serviceCollection.AddScoped<IIdGenerator, IdGenerator>();
            serviceCollection.AddScoped<CartService>();
         
            return serviceCollection;
        }

        public static IServiceCollection AddRepositoryService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRoleRepository, RoleRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IUserRoleRepository, UserRoleRepository>();
            serviceCollection.AddScoped<IFirmParamRepository,FirmParamRepository>();
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddScoped<IOrdFicheRepository, OrdFicheRepository>();
            serviceCollection.AddScoped<IDocumentNoRepository, DocumentNoRepository>();

            return serviceCollection;
        }
        public static IServiceCollection AddBusinessService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRoleService, RoleManager>();
            serviceCollection.AddScoped<IUserService,UserManager>();
            serviceCollection.AddScoped<IUserRoleService, UserRoleManager>();
            serviceCollection.AddScoped<IClientCardService,ClientCardManager>();
            serviceCollection.AddScoped<IProductServices,ProductManager>();
            serviceCollection.AddScoped<ICategoryService,CategoryManager>();
            serviceCollection.AddScoped<IOrderService,OrderManager>();
            serviceCollection.AddScoped<IFirmParamService,FirmParamManager>();
            serviceCollection.AddScoped<IDocumentNoService,DocumentNoManager>();
            return serviceCollection;
        }
    }
}
