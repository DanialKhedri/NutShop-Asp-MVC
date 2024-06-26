using Application.Extensions.KaveNegar;
using Application.Services.implements;
using Application.Services.Interfaces;
using Domain.IRepository;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add dbContext

builder.Services.AddDbContext<DataContext>(option =>
{

    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));

}
);

//HttpAccessor
builder.Services.AddHttpContextAccessor();

//Configure KaveNegar model from appsetting 
builder.Services.Configure<KaveNegarInfoModel>(builder.Configuration.GetSection("KaveNegarInfo"));


//User serv and Rep
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


//Product Serv and rep
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


//Category Ser and Rep
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


//Order Serv And Rep
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


//Role Serv And Rep
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();


//Shop Serv And Rep
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<IShopService, ShopService>();


//Aboutus Serv And Rep
builder.Services.AddScoped<IAboutUsService, AboutUsService>();
builder.Services.AddScoped<IAboutUsRepository, AboutUsRepository>();

//Sms Service
builder.Services.AddScoped<ISmsService, SmsService>();

builder.Services.AddScoped<IPaymentService, PaymentService>();


#region Authentication


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
// Add Cookie settings
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute
    (
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );


app.MapAreaControllerRoute(
    name: "default",
    areaName: "SitePanel",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    






app.Run();
