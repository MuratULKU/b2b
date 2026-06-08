using B2C.Components.Base;
using Business.Abstract;
using Business.Concrete;
using Business.SingletonServices;
using Core.Logger;
using CoreUI;
using CoreUI.BackOrder;
using CoreUI.BackOrder.Mikro;
using CoreUI.Components.NotificationService;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.EFCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddUtiliesService();
builder.Services.AddBusinessServices(builder.Configuration);
#region Dinamik Veritabaný Seçimi
var provider = builder.Configuration["DatabaseProvider"] ?? "SqlServer";

// ? AddDbContext ÖNCE ekle
builder.Services.AddDbContext<RepositoryContext>(options =>
{
    if (provider == "Sqlite")
    {
        var connectionString = builder.Configuration.GetConnectionString("Sqlite");
        options.UseSqlite(connectionString, x => x
            .MigrationsAssembly("DataAccess")
            .MigrationsHistoryTable("__EFMigrationsHistory"));
    }
    else
    {
        var connectionString = builder.Configuration.GetConnectionString("SqlServer")
            ?? throw new InvalidOperationException("SqlServer connection string eksik");
        options.UseSqlServer(
            connectionString,
            x => x
                .MigrationsAssembly("DataAccess")
                .MigrationsHistoryTable("__EFMigrationsHistory_SqlServer"));
    }
});
#endregion

// ? AddDbContext'ten SONRA register et
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();
builder.Services.AddRepositoryService();
builder.Services.AddBusinessService();

builder.Services.AddHttpClient();
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

builder.Services.AddSingleton<FirmParameter>();
builder.Services.AddBackOrederServices(builder.Configuration);
builder.Services.AddSingleton<BackOrder>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<BackOrder>());
builder.Services.AddHttpClient<BackOrder>(service =>
    service.BaseAddress = new Uri(builder.Configuration["ApiService:Url"]));
builder.Services.AddScoped<NotificationService>();

builder.Services.AddSingleton<LogoApiStrategy>();
builder.Services.AddSingleton<MikroApiStrategy>();
builder.Services.AddSingleton<UlkuApiStrategy>();
builder.Services.AddSingleton<ApiStrategyFactory>();

builder.Services.AddSingleton<ILoggerService>(p => new FileLogger("app.log"));

var apiUrl = builder.Configuration["ApiService:Url"] ?? throw new InvalidOperationException("ApiService:Url yapýlandýrmasý eksik");

builder.Services.AddHttpClient<ITokenService, TokenService>(client =>
    client.BaseAddress = new Uri(apiUrl));

builder.Services.AddHttpClient<IMikroClientService, MikroClientService>(client =>
    client.BaseAddress = new Uri(apiUrl));

builder.Services.AddHttpClient<IMikroProductService, MikroProductService>(client =>
    client.BaseAddress = new Uri(apiUrl));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.InitializeDatabase();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("Ok", "payment/ok/{paymentId:Guid?}",
        defaults: new { action = "OkUrl", controller = "Payment" });
    endpoints.MapControllerRoute("Fail", "payment/fail/{paymentId:Guid?}",
        defaults: new { action = "FailUrl", controller = "Payment" });
    endpoints.MapControllerRoute("Confirm", "payment/confirm/{paymentId:Guid?}",
        defaults: new { action = "Confirm", controller = "Payment" });
    endpoints.MapControllerRoute("Callback", "payment/callback/{paymentId:Guid?}",
        defaults: new { action = "Callback", controller = "Payment" });
    endpoints.MapRazorPages();
    endpoints.MapDefaultControllerRoute();
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();