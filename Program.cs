using GlassApplication.Models.Abstract;
using GlassApplication.Models;
using GlassApplication.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace GlassApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<I�r�nService, �r�nService>();
            builder.Services.AddScoped<I�r�nRepository, �r�nRepository>();
            builder.Services.AddScoped<ISipari�Repository, Sipari�Repository>();
            builder.Services.AddScoped<ISipari�Service, Sipari�Service>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDbContext<GW_SISTEM>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GWSISTEMConnection")));
            builder.Services.AddDbContext<GW_TEST_2025>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GWTest2025Connection")));

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dakika oturum s�resi
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
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

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
