using Bl.Data;
using Bl.IRepository;
using Bl.IRepository.ServicesRepository;
using Bl.Seeds;
using Bl.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LapStoreContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/*builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<LapStoreContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
});*/
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
{
    Options.Password.RequireDigit = false;
    Options.Password.RequiredUniqueChars = 0;
    Options.Password.RequiredLength =6;
    Options.Password.RequireNonAlphanumeric = false;
    Options.Password.RequireUppercase = false;
    Options.Password.RequireLowercase = false;
    Options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<LapStoreContext>();


builder.Services.AddScoped<IServicesColor, ServicesColor>();
builder.Services.AddScoped<IServicesCategory, ServicesCategory>();
builder.Services.AddScoped<IServicesOs, ServicesOs>();
builder.Services.AddScoped<IServicesTypee, ServicesTypee>();
builder.Services.AddScoped<IServicesSlider, ServicesSlider>();
builder.Services.AddScoped<IServicesPage, ServicesPage>();
builder.Services.AddScoped<IServicesSetting, ServicesSetting>();
builder.Services.AddScoped<IServicesProduct, ServicesProduct>();
builder.Services.AddScoped<IServicesOrder, ServicesOrder>();
builder.Services.AddScoped<IServicesOrderItem, ServicesOrderItem>();
builder.Services.AddScoped<IServicesUser, ServicesUser>();




builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.Name = "Cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
    options.LoginPath = "/Account/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;

    options.SlidingExpiration = true;
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        await DefaultRole.SeedRoleAsync(roleManager);
        await DefaultUser.SeedUserAsync(userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}


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
app.UseSession();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
