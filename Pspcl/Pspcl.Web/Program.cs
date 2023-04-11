
using Serilog;
//using Serilog.Sinks.RollingFile;
using Serilog.Sinks.File;
using Microsoft.AspNetCore.Builder.Extensions;


namespace Pspcl.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {



            var builder = WebApplication.CreateBuilder(args);




            var _logger = new LoggerConfiguration()
            .MinimumLevel.Information().WriteTo
                         .Map("UtcDateTime", DateTime.UtcNow.ToString("ddMMyyyy"), (UtcDateTime, wt) => wt.File($"Logs/Log_{UtcDateTime}.txt"))
                         .CreateLogger();

            builder.Logging.AddSerilog(_logger);



            builder.Services.AddControllersWithViews();


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
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();


        }
    }
}