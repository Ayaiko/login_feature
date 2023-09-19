﻿using log4net;
using log4net.Config;
using LoginWebApp.Data;
using LoginWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace LoginWebApp
{
    public class Startup
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Startup));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));

        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<LoginDbContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<LoginDbContext>();
            services.AddSingleton<ILog>(_ => LogManager.GetLogger(typeof(Startup)));

            services.AddMvc();
            services.AddControllersWithViews();

        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
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

            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        public IConfiguration Configuration { get; set; }
    }
}
