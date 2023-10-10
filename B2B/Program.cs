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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages(); // use razor pages without controller
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("B2B"));
});

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//repository services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPriceListRepository, PriceListRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFirmParamRepository, FirmParamRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();


//back order services
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddSingleton<FirmParameterService>();
builder.Services.AddSingleton<UserManager>();
builder.Services.AddSingleton<IBackOrderProductService, BackOrderProductService>();
builder.Services.AddSingleton<IBackOrderCategoryService, BackOrderCategoryService>();
builder.Services.AddSingleton<IBackOrderPriceListService, BackOrderPriceListService>();
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
    endpoints => endpoints.MapRazorPages()
    ); ;
app.Run();
