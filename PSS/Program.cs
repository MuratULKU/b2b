
using B2C.Components.Base;
using Business.Abstract;
using Business.Concrete;
using Business.SingletonServices;
using CoreUI.BackOrder;
using CoreUI.Components.Base;
using CoreUI.Components.NotificationService;
using CoreUI.Data;

using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.EFCore;

using Microsoft.AspNetCore.Components.Authorization;

using Microsoft.EntityFrameworkCore;


using PSS.Components.UserPanel;
using PSS.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddUtiliesService();
builder.Services.AddRepositoryService();
builder.Services.AddBusinessService();
builder.Services.AddHttpClient();
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

builder.Services.AddDbContext<RepositoryContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("PSS")),
     ServiceLifetime.Transient);
builder.Services.AddScoped<UserRoleManager>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserIdentityProcessor, UserIdentityProcessor>();
builder.Services.AddSingleton<FirmParameter>();
builder.Services.AddBackOrederServices();
builder.Services.AddHostedService<BackOrder>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IUnitofWork, UnitofWork>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
