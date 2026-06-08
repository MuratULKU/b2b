using _3DPayment;
using Business.Abstract;
using Business.Concrete;
using CoreUI.Components.Confirm;
using CoreUI.Components.UserPanel;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.EFCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
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
            services.AddScoped<ICreditCardInstallmentService, CreditCardInstallmentManager>();
            services.AddScoped<IPaymentProviderFactory, PaymentProviderFactory>();

            //banka servisleri

            //virtualpos servisleri

            //payment servileri

            return services;
        }
    }
}
