using Lamar.Microsoft.DependencyInjection;
using Pspcl.Web.Lamar;
using Microsoft.EntityFrameworkCore;
using Pspcl.DBConnect;
using Pspcl.Core.Domain;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DBConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Host.UseLamar((context, registry) =>
{
    registry.IncludeRegistry<ApplicationRegistry>();
    registry.AddControllers();
});

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
app.UseAuthentication(); ;

app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Account}/{action=Login}/{id?}");
//    endpoints.MapAreaControllerRoute(
//      name: "Identity",
//      areaName: "Identity",
//      pattern: "Identity/{controller=Account}/{action=Login}/{id?}"
//    );
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.MapRazorPages();
app.Run();
