using B2B.BackOrder;
using B2B.Components.Login;
using B2B.Data;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.EFCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using SanalMagaza.Business.Concrete;
using SanalMagaza.DataAccess.Concrete;
using _3DPayment;
using System.Collections.ObjectModel;
using B2B.Components.NotificationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages(); // use razor pages without controller
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("B2B")

    );  
   
});

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();
//notification service
builder.Services.AddScoped<NotificationService>();

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//repository services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPriceListRepository, PriceListRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFirmParamRepository, FirmParamRepository>();
builder.Services.AddScoped<IFirmParamService, FirmParamManager>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBankCardRepository, BankCardRepository>();
builder.Services.AddScoped<IBankCardService, BankCardManager>();
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();
builder.Services.AddScoped<IVirtualPosRepository, VirtualPosRepository>();
builder.Services.AddScoped<IVirtualPosService, VirtualPosManager>();
builder.Services.AddScoped<ICreditCardInstallmentRepository, CreditCardInstallmentRepository>();
builder.Services.AddScoped<ICreditCardInstallmentService, CreditCardInstallmentManager>();
builder.Services.AddScoped<IBankParameterRepository, BankParameterRepository>();
builder.Services.AddScoped<IBankParameterService, BankParameterManager>();
builder.Services.AddScoped<ICreditCardPrefixRepository, CreditCardPrefixRepository>();
builder.Services.AddScoped<ICreditCardPrefixService, CreditCardPrefixManager>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IProductAmountRepository, ProductAmountRepository>();
builder.Services.AddScoped<IFirmDocRepository, FirmDocRepository>();
builder.Services.AddScoped<IDocumentNoRepository, DocumentNoRepository>();
builder.Services.AddSingleton<PostFormService>();

//back order services
builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddSingleton<FirmParameterService>();
builder.Services.AddSingleton<UserManager>();

builder.Services.AddHttpClient();

//payment singleton servis örnek...
builder.Services.AddPaymentServices();

builder.Services.AddSingleton<IBackOrderProductService, BackOrderProductService>();
builder.Services.AddSingleton<IBackOrderCategoryService, BackOrderCategoryService>();
builder.Services.AddSingleton<IBackOrderPriceListService, BackOrderPriceListService>();
builder.Services.AddSingleton<IBackOrderProductAmountService, BackOrderProductAmountService>();
builder.Services.AddSingleton<IBackOrderOder, BackOrderOrder>();

builder.Services.AddHostedService<BackOrder>();
builder.Services.AddLogging(
    options =>
    {
        options.AddConsole();
        options.AddDebug();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.InitializeDatabase();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseEndpoints(
    endpoints =>
    {
        endpoints.MapControllerRoute(
        name: "Confirm",
        pattern: "payment/confirm/{paymentId:Guid?}",
        defaults: new { action = "Confirm", controller = "Payment" });
        endpoints.MapControllerRoute(
        name: "Callback",
        pattern: "payment/callback/{paymentId:Guid?}",
        defaults: new { action = "Callback", controller = "Payment" });
        endpoints.MapRazorPages();
        endpoints.MapDefaultControllerRoute();
    }
    );
app.Run();
