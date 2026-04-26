using Microsoft.EntityFrameworkCore;
using HastaneMVC.Data;
using HastaneMVC.Services;
namespace HastaneMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddDbContext<HastaneContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HastaneBaglantisi")));

            builder.Services.AddScoped<RandevuService>();  // services altında oluşturduğumuz randevuservice in çalışması için bu yoksa hata veriyor 

            // dependcy injection için gerekli bu olmazsa dependcy injection kısımları çalışmaz



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
               // url yapılandırması 
            app.Run();
        }
    }
}
