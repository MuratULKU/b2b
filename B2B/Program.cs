
using CoreUI.Components.NotificationService;
using DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using Business.Concrete;
using CoreUI.BackOrder;

using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Components.Authorization;
using B2B.Components.UserPanel;
using Business.Abstract;
using CoreUI.Components.Base;
using CoreUI.Components.Utilities;

using B2B.Data;
using SanalMagaza.DataAccess.Concrete;
using Core.Logger;
using SanalMagaza.Business.Concrete;
using _3DPayment;
using Business.SingletonServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RepositoryContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("B2B")),
     ServiceLifetime.Transient);

builder.Services.AddSingleton<ILoggerService>(provider =>
            new FileLogger("app.log"));
//utilities

builder.Services.AddSingleton<BootstrapClassProvider>();
builder.Services.AddScoped<SessionManager>();
builder.Services.AddScoped<IIdGenerator, IdGenerator>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<NotificationService>();


builder.Services.AddScoped<IDocumentNoRepository, DocumentNoRepository>();  
builder.Services.AddScoped<IDocumentNoService,DocumentNoManager>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();  
builder.Services.AddScoped<IUserRoleService,UserRoleManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductServices, ProductManager>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IFirmParamRepository, FirmParamRepository>();
builder.Services.AddScoped<IFirmParamService, FirmParamManager>();
builder.Services.AddScoped<IOrderService,OrderManager>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientCardService, ClientCardManager>();
builder.Services.AddScoped<ICharValRepository, CharValRepository>();
builder.Services.AddScoped<ICharValService,CharValManager>();
builder.Services.AddScoped<IBankCardRepository, BankCardRepository>();
builder.Services.AddScoped<IBankCardService, BankCardManager>();
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();  
builder.Services.AddScoped<ICreditCardService, CreditCardManager>();
builder.Services.AddScoped<ICreditCardInstallmentRepository, CreditCardInstallmentRepository>();
builder.Services.AddScoped<ICreditCardInstallmentService, CreditCardInstallmentManager>();
builder.Services.AddScoped<ICreditCardPrefixService, CreditCardPrefixManager>();
//builder.Services.AddScoped<IVirtualPosParameterRepository, VirtualPosParameterRepository>();
builder.Services.AddScoped<IVirtualPosParameterService, VirtualPosParameterManager>();
builder.Services.AddScoped<IVirtualPosRepository, VirtualPosRepository>();
builder.Services.AddScoped<IVirtualPosService,VirtualPosManager>();
builder.Services.AddScoped<ICardBrandService,CardBrandManager>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService,RoleManager>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyManager>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService,PaymentManager>();
builder.Services.AddScoped<IPaymentProviderFactory, PaymentProviderFactory>();

builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<UserRoleManager>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserIdentityProcessor, UserIdentityProcessor>();
builder.Services.AddSingleton<FirmParameter>();
builder.Services.AddBackOrederServices();
builder.Services.AddHostedService<BackOrder>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IUnitofWork, UnitofWork>();



builder.Services.AddHttpClient();


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
