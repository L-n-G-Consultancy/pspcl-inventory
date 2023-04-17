using Lamar.Microsoft.DependencyInjection;
using Pspcl.Web.Lamar;

using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

using Microsoft.EntityFrameworkCore;
using Pspcl.DBConnect;
using Pspcl.Web;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DBConnectionString");
builder.Services.AddDbContext<PspclDbContext>(options =>
    options.UseSqlServer(connectionString));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
