using B2C.Components.Base;
using B2C.Components.UserPanel;
using Business.Abstract;
using Business.Concrete;
using Business.SingletonServices;
using Core.Logger;
using CoreUI;
using CoreUI.BackOrder;
using CoreUI.BackOrder.Mikro;
using CoreUI.Components.Confirm;
using CoreUI.Components.NotificationService;

using CoreUI.Data;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.EFCore;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer; // Ekleyin: UseSqlServer uzantýsý için gerekli

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();
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

// repository context
builder.Services.AddUtiliesService();
builder.Services.AddRepositoryService();
builder.Services.AddBusinessService();
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();
//back order service

builder.Services.AddSingleton<FirmParameter>();
builder.Services.AddSingleton<ConfirmDialogService>();

builder.Services.AddBackOrederServices(builder.Configuration);
builder.Services.AddHttpClient<BackOrder>(service => service.BaseAddress = new Uri(builder.Configuration["ApiService:Url"]));

builder.Services.AddScoped<NotificationService>();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<FirmParameterService>();
//user aAuthentication
builder.Services.AddBusinessServices(builder.Configuration);

builder.Services.AddHttpClient<ITokenService, TokenService>(client =>
   client.BaseAddress = new Uri(builder.Configuration["ApiService:Url"]));

builder.Services.AddHttpClient<IMikroClientService, MikroClientService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiService:Url"]);
});

builder.Services.AddHttpClient<IMikroProductService, MikroProductService>(client =>
   client.BaseAddress = new Uri(builder.Configuration["ApiService:Url"]));


var app = builder.Build();
//initalize database
app.InitializeDatabase();
app.UseRequestLocalization("tr-TR");
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

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

app.Run();
