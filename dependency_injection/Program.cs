using dependency_injection.Services;
using dependency_injection.Services.Interfaces;

namespace dependency_injection

    
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // interface çağırma
            builder.Services.AddScoped<ILog>(p=> new Textlog()); // loglama için hangisi kullanılacaksa o buraya gelecek 

            // Add services to the container.
            builder.Services.AddControllersWithViews();  // services eklme için 

            // bunlar singletion olarak eklendi şuan
            builder.Services.Add(new ServiceDescriptor(typeof(Consolelog), new Consolelog(3)));// consolelog servisini ekledik
            builder.Services.Add(new ServiceDescriptor(typeof(Textlog), new Textlog()/*, at sonra lifetime tanımla  services life time kısmı burada tanımlanır  singletondan olamsın diye   */ )); //  Transient  , Scoped buradan bildirilir

            // yukarıdaki çok kullanılmaz bunun yerine

            //     builder.Services.AddSingleton<Consolelog>();  // bütün isteklerde tek nesne üretir bunu kullanır new T yi baz alır    runtime da bu patlatır

            builder.Services.AddTransient<Consolelog>(p => new Consolelog(5));   // tek nesne üretir her isteği bu tek üretilen nesne karşılar

            builder.Services.AddScoped<Textlog>();   //  tüm isteklerede ayrı bir nesne üretir gönderir   

            builder.Services.AddTransient<Consolelog>(p => new Consolelog(5));  // her isteğin her talepine nesne üretir
            // servisler interface altında tutulur   t
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
