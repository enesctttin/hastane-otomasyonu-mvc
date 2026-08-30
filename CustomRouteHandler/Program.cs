using CustomRouteHandler.Handlers;

namespace CustomRouteHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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

            // herhangi view render edilecek view açılacak  veri tabanından verileri alıp kullanıcıya sunacaksın bu tarz yerlde bu kullanılmaz genel geçer konularda klasik controller mekanizması kullanılır 
            // gelen isteği controller harici bir sınıfa nasıl göndeririz  rotaları tanımladığımız program.cs de bu işlem yapılacak

            //app.UseEndpoints(endpoints =>
            //{
            //    // /anasayafa adresini HomeController -> Index action'ına yönlendirir
            //    // async  geriye task döndürür   bu kural hacı burada 
            //    endpoints.Map("example-route",/*buraya metod tanımlanmalı isteği karşılayacak */async c =>
            //    {

            //        //https://localhost:5001/example-route
            //        //endpoint'e gelen herhangi bir istek  Controllerdan ziyade buradaki fonksiyon tarafından karşılanacak


            //    });
            //    endpoints.Map("example-route", new ExampleHandler().Handler());




            //    app.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");

            // yukarısı patlıyor 
            string imagesFolder = app.Environment.WebRootPath;

            app.UseEndpoints(endpoints =>
            {
                // /example-route adresine gelen istek ExampleHandler tarafından karşılanır
                endpoints.Map("example-route", new ExampleHandler().Handler());


                // hangi dizinde reseimlerin olduğu dizini dönecek konumunu     
                endpoints.Map("image/{imageName}",new ImageHandler().Handler(imagesFolder));  //urle  https://localhost:7136/image/resim.png?w=150

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            app.Run();
            }
        }
    }


