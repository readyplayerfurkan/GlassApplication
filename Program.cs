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
            builder.Services.AddScoped<IÜrünRepository, ÜrünRepository>();
            builder.Services.AddScoped<ISiparişRepository, SiparişRepository>();
            builder.Services.AddScoped<ISiparişService, SiparişService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<CariService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDbContext<AuthorizationDatabase>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AuthorizationDatabaseConnection")));
            builder.Services.AddDbContextFactory<ContentDatabase>(options => { }); 

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
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
