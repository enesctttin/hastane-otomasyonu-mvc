using MiddlewareExample.Web.Middlewares;

namespace MiddlewareExample.Web
{
    public class    Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Configure the HTTP request pipeline.  // middeleware lara configure nin altında başlar  ve Use  adıyla başlar
            // middleware larda tetiklenme sıraları önemlidir ilk giriş sonra yetki kontrolü 
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //   gelen request e karşılık dönecek olan respoonsse öncesi islemler





            #region map ve run kullanımı
            /*

            app.Use(async(context, next) =>
            {
                
                await context.Response.WriteAsync("  Middleware 1 Başladı\n");

                await next (); // Sonraki middleware'e geçiş yapar

                await context.Response.WriteAsync("  Middleware 1 Bitti\n"); 

            });


            app.Use(async (abc, ne) =>
            {

                await abc.Response.WriteAsync("  Middleware 2 Başladı\n");

                await ne(); // Sonraki middleware'e geçiş yapar

                await abc.Response.WriteAsync("  Middleware 3 Bitti\n");

            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("  Use yerine Run ile yazılan middleware sonlandırıcı middelware   \n");
            });

            */

            #endregion



            // MapWhen ile urlden gelen isteklere göre middleware yazabiliriz  if bloğu gibi çalışır  /ne_gelirse o gelirse bu middleware çalışır gibi
/*
            app.MapWhen(c => c.Request.Method == "GET", builder =>
            {
                builder.Use(async (context, task) =>
                {
                    Console.WriteLine("start use middleware2");
                    await task.Invoke();
                    Console.WriteLine("stop use middleware2");

                });




            });
*/

            /*
            
            // map metodu sadece gelen request i /ne_gelirse
            app.Map("ornek", app =>
            {
                app.Run(async caaax =>
                {
                    await caaax.Response.WriteAsync("  Map ile yazılan middleware sadece /ornek adresine gelen isteklere cevap verir\n");
                });


            });

            */




            app.UseMiddleware<WhiteIpAddressControlMiddleware>(); 








            app.UseRouting();

            app.UseAuthorization();



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
