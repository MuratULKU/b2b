

using B2C.Components.Base;
using B2C.Components.UserPanel;
using Business.Concrete;
using CoreUI.BackOrder;
using CoreUI.Components.NotificationService;

using CoreUI.Data;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.EFCore;

using Microsoft.AspNetCore.Components.Authorization;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();
//database conneciton
builder.Services.AddDbContext<RepositoryContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("B2C")),

 ServiceLifetime.Transient);

// repository context
builder.Services.AddUtiliesService();
builder.Services.AddRepositoryService();
builder.Services.AddBusinessService();
builder.Services.AddScoped<IUnitofWork, UnitofWork>();
//back order service

builder.Services.AddSingleton<FirmParameterService>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddBackOrederServices();
builder.Services.AddHostedService<BackOrder>();

builder.Services.AddScoped<NotificationService>();
builder.Services.AddHttpClient();

//user aAuthentication
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

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
