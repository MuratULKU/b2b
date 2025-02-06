
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
using B2B.Components.NotificationService;
using B2B.Logger.FileLogger;

using B2B.Components.Utilities;
using Microsoft.AspNetCore.Components.Server.Circuits;
using CoreUI.BackOrder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.

//builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages(); // use razor pages without controller
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RepositoryContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("B2B")),
  
 ServiceLifetime.Transient);

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Logging.AddFileLoger();
builder.Services.AddUtiliesService();
//notification service
builder.Services.AddScoped<NotificationService>();


builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//repository services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleManager>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserRoleService, UserRoleManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPriceListRepository, PriceListRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFirmParamRepository, FirmParamRepository>();
builder.Services.AddScoped<IFirmParamService, FirmParamManager>();

builder.Services.AddScoped<IOrdFicheRepository, OrdFicheRepository>();
builder.Services.AddScoped<IBankCardRepository, BankCardRepository>();
builder.Services.AddScoped<IBankCardService, BankCardManager>();
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();
builder.Services.AddScoped<ICreditCardService, CreditCardManager>();
builder.Services.AddScoped<IVirtualPosRepository, VirtualPosRepository>();
builder.Services.AddScoped<IVirtualPosService, VirtualPosManager>();
builder.Services.AddScoped<ICreditCardInstallmentRepository, CreditCardInstallmentRepository>();
builder.Services.AddScoped<ICreditCardInstallmentService, CreditCardInstallmentManager>();
builder.Services.AddScoped<IVirtualPosParameterRepository, VirtualPosParameterRepository>();
//builder.Services.AddScoped<IVirtualPosParameterService, BankParameterManager>();
builder.Services.AddScoped<ICreditCardPrefixRepository, CreditCardPrefixRepository>();
builder.Services.AddScoped<ICreditCardPrefixService, CreditCardPrefixManager>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentManager>();
builder.Services.AddScoped<IProductAmountRepository, ProductAmountRepository>();
builder.Services.AddScoped<IFirmDocRepository, FirmDocRepository>();
builder.Services.AddScoped<ICharSetRepository, CharSetRepository>();
builder.Services.AddScoped<ICharCodeRepository, CharCodeRepository>();
builder.Services.AddScoped<ICharValRepository, CharValRepository>();
builder.Services.AddScoped<ICharAsgnRepository, CharAsgnRepository>();
builder.Services.AddScoped<IDocumentNoRepository, DocumentNoRepository>();
builder.Services.AddSingleton<PostFormService>();

//back order services
builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddSingleton<FirmParameterService>();
builder.Services.AddScoped<UserManager>();

builder.Services.AddHttpClient();

//payment singleton servis örnek...
builder.Services.AddPaymentServices();
builder.Services.AddSingleton<CircuitHandler>(new CircuitHandlerService());

builder.Services.AddBackOrederServices();
builder.Services.AddHostedService<BackOrder>();
builder.Services.AddLocalization();
// firm paramm service varsayýlan yüklemesi için yapýlmýþtý.
//var serviceProvider = builder.Services.BuildServiceProvider();
//ProductExtension.Configure(serviceProvider.GetService<IFirmParamService>());

var app = builder.Build();
//app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames=new List<string> {"login.html"} });
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseRequestLocalization("tr-TR");
app.InitializeDatabase();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
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
