

using Microsoft.Extensions.DependencyInjection;

namespace _3DPayment
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPaymentServices(this IServiceCollection services)
        {
            
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddSingleton<IPaymentProviderFactory, PaymentProviderFactory>();

            return services;
        }
    }
}
