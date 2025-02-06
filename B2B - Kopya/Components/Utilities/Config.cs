using B2B.Components.Preload;
using Blazorise;

namespace B2B.Components.Utilities
{
    public static class Config
    {
        public static IServiceCollection AddUtiliesService (this  IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<BootstrapClassProvider>();
            serviceCollection.AddScoped<IIdGenerator, IdGenerator>();

            serviceCollection.AddScoped<B2B.Components.Modal.ModalService>();
            serviceCollection.AddScoped<PreloadService>();

            return serviceCollection;
        }
    }
}
