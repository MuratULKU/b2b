
using CoreUI.Components.NotificationService;
using DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using Business.Concrete;
using CoreUI.BackOrder;

using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Components.Authorization;

using Business.Abstract;
using CoreUI.Components.Base;
using CoreUI.Components.Utilities;

using B2B.Data;
using SanalMagaza.DataAccess.Concrete;
using Core.Logger;
using SanalMagaza.Business.Concrete;
using _3DPayment;
using Business.SingletonServices;
using System.Globalization;
using CoreUI.Components.Confirm;
using CoreUI.Components.LoadingService;
using CoreUI.Components.UserPanel;
using CoreUI;
using CoreUI.BackOrder.Mikro;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("tr-TR");


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();

// TempData için Session'ý aktif etmelisin
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var provider = builder.Configuration["DatabaseProvider"];

builder.Services.AddDbContext<RepositoryContext>(options =>
{
    if (provider == "Sqlite")
    {
        options.UseSqlite(
            builder.Configuration.GetConnectionString("Sqlite"),
            // SQLite'a özel migration dosyalarýný DataAccess.EFCore altýndaki SqliteMigrations klasöründe toplar
            x => x.MigrationsAssembly("DataAccess"));
    }
    else
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("SqlServer"),
            // SQL Server'a özel migration
            // dosyalarýný DataAccess.EFCore altýndaki SqlServerMigrations klasöründe toplar
            x => x.MigrationsAssembly("DataAccess"));
    }
});
builder.Services.AddSingleton<ILoggerService>(provider =>
            new FileLogger("app.log"));
//utilities

builder.Services.AddSingleton<BootstrapClassProvider>();
builder.Services.AddScoped<SessionManager>();
builder.Services.AddScoped<IIdGenerator, IdGenerator>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddSingleton<LoadingService>();

builder.Services.AddScoped<IDocumentNoService, DocumentNoManager>();
builder.Services.AddScoped<IUserRoleService, UserRoleManager>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IFirmParamRepository, FirmParamRepository>();
builder.Services.AddScoped<IFirmParamService, FirmParamManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<IClientCardService, ClientCardManager>();
builder.Services.AddScoped<ICharValService, CharValManager>();
builder.Services.AddScoped<IVirtualPosService, VirtualPosManager>();
builder.Services.AddScoped<ICreditCardInstallmentService, CreditCardInstallmentManager>();
builder.Services.AddScoped<ICreditCardPrefixService, CreditCardPrefixManager>();
builder.Services.AddScoped<IBankCardService, BankCardManager>();
builder.Services.AddScoped<ICardBrandService, CardBrandManager>();
builder.Services.AddScoped<ICreditCardService, CreditCardManager>();
builder.Services.AddScoped<IPaymentService, PaymentManager>();
builder.Services.AddScoped<IVirtualPosParameterService, VirtualPosParameterManager>();
builder.Services.AddScoped<IClFicheService, ClFicheManager>();
builder.Services.AddScoped<ICompanyService, CompanyManager>();
builder.Services.AddScoped<IRoleService, RoleManager>();


builder.Services.AddScoped<IFirmDocService, FirmDocManager>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddBusinessServices(builder.Configuration);

builder.Services.AddSingleton<FirmParameter>();
builder.Services.AddBackOrederServices(builder.Configuration);
builder.Services.AddHostedService<BackOrder>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();
builder.Services.AddHttpClient<ITokenService, TokenService>(client =>
   client.BaseAddress = new Uri(builder.Configuration["ApiService:Url"]));


builder.Services.AddHttpClient<BackOrder>(service =>service.BaseAddress = new Uri(builder.Configuration["ApiService:Url"]));
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IOrdFicheRepository, OrdFicheRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddSingleton<LogoApiStrategy>();
builder.Services.AddSingleton<MikroApiStrategy>();
builder.Services.AddSingleton<UlkuApiStrategy>();
builder.Services.AddSingleton<ApiStrategyFactory>();

builder.Services.AddHttpClient<IMikroClientService, MikroClientService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiService:Url"]);
});
builder.Services.AddHttpClient<IMikroProductService, MikroProductService>(client =>
   client.BaseAddress = new Uri(builder.Configuration["ApiService:Url"]));


var app = builder.Build();

app.InitializeDatabase();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "Ok",
    pattern: "payment/ok/{paymentId:Guid?}",
    defaults: new { action = "OkUrl", controller = "Payment" });
    endpoints.MapControllerRoute(
     name: "Fail",
     pattern: "payment/fail/{paymentId:Guid?}",
     defaults: new { action = "FailUrl", controller = "Payment" });
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

});
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();
