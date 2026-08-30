using Microsoft.EntityFrameworkCore;
using HastaneMVC.Data;
using HastaneMVC.Services;
using HastaneMVC.Constraints;
namespace HastaneMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // program çalışırken sırayla çalışacak                                         bu default route pattern de ki id nin patterni 
            builder.Services.Configure<RouteOptions>(options => options.ConstraintMap.Add("default", typeof(CustomConstraint)));

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddDbContext<HastaneContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HastaneBaglantisi")));

            builder.Services.AddScoped<RandevuService>();  // services altında oluşturduğumuz randevuservice in çalışması için bu yoksa hata veriyor 
            builder.Services.AddScoped<DoktorService>(); 
            // dependcy injection için gerekli bu olmazsa dependcy injection kısımları çalışmaz



            var app = builder.Build(); // uygulamayı oluşturur ve yapılandırır




            // development ortamında çalışırken hata sayfası verir ama production ortamında
            // çalışırken hata sayfası vermez onun yerine kendi belirlediğimiz bir sayfaya yönlendiririz o sayfa home controllerda error sayfasıdır
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); // HSTS (HTTP Strict Transport Security) güvenlik önlemi olarak kullanılır, tarayıcının sadece HTTPS üzerinden iletişim kurmasını sağlar
            }
            app.UseHttpsRedirection();// urlde http varsa otomatik https yapar
            app.UseStaticFiles(); // Static dosyaların (CSS, JavaScript, resimler vb.) sunulmasını sağlar. Bu middleware, wwwroot klasöründeki dosyalara erişimi mümkün kılar.

            app.UseRouting(); // Routing middleware'i, gelen HTTP isteklerini uygun denetleyici eylemlerine yönlendirmek için kullanılır. Bu middleware, URL desenlerini tanımlayarak hangi denetleyici ve eylemin çağrılacağını belirler.

            app.UseAuthorization(); //kimlik dığrulama yetkilnedirme admın admine doktor doktora sekretere sekrete  controllerde  en üste  namesapce altına [Authorize] yazınca etkinleşir

            app.UseEndpoints(endpoints =>
            {
                // /anasayafa adresini HomeController -> Index action'ına yönlendirir
                endpoints.MapControllerRoute(
                    name: "default2",
                    pattern: "anasayafa",
                    defaults: new
                    {
                        controller = "Home",
                        action = "Index"
                    });
            });



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id:default?}");
               // url yapılandırması 
            app.Run();
        }
    }
}
