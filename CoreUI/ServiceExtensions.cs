using _3DPayment;
using Business.Abstract;
using Business.Concrete;
using CoreUI.Components.Confirm;
using CoreUI.Components.UserPanel;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Components.Authorization;
using SanalMagaza.Business.Concrete;
using SanalMagaza.DataAccess.Concrete;

namespace CoreUI
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IUserIdentityProcessor, UserIdentityProcessor>();
            services.AddSingleton<ConfirmDialogService>();
            services.AddScoped<UserRoleManager>();
            services.AddScoped<UserManager>();
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(8);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //kredi kartı Servisleri
            services.AddScoped<IPaymentProviderFactory, PaymentProviderFactory>();
            services.AddScoped<IVirtualPosService, VirtualPosManager>();
            services.AddScoped<ICreditCardPrefixService, CreditCardPrefixManager>();
            services.AddScoped<ICardBrandService, CardBrandManager>();
            services.AddScoped<IBankCardService, BankCardManager>();
            services.AddScoped<ICreditCardService, CreditCardManager>();

            //banka servisleri
            services.AddScoped<ICreditCardRepository, CreditCardRepository>();

            services.AddScoped<ICreditCardInstallmentRepository, CreditCardInstallmentRepository>();
            services.AddScoped<ICreditCardInstallmentService, CreditCardInstallmentManager>();
            //virtualpos servisleri
            services.AddScoped<IVirtualPosParameterService, VirtualPosParameterManager>();
            services.AddScoped<IVirtualPosRepository, VirtualPosRepository>();

            //payment servileri
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentManager>();

            services.AddScoped<IClFicheRepository, ClFicheRepository>();
            services.AddScoped<IClFicheService, ClFicheManager>();
            return services;
        }
    }
}
