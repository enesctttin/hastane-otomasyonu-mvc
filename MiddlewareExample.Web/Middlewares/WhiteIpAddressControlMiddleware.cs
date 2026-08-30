using System.Net;

namespace MiddlewareExample.Web.Middlewares
{
    public class WhiteIpAddressControlMiddleware
    {

        // 
        // bu yaptığımızı mapwhen  ile yap next kullan run kullan 
        //


        private readonly RequestDelegate _requestDelegate;

        private const string WhiteIpAddress = "::1";

        // Middleware'ler, HTTP isteklerini işlemek ve yanıtları oluşturmak için kullanılan yazılım bileşenleridir. Bir middleware, gelen bir HTTP isteğini alır, üzerinde işlem yapar ve ardından isteği bir sonraki middleware'e ileterek zinciri devam ettirir. Bu süreç, uygulamanın istekleri nasıl ele aldığına dair esneklik sağlar.
        public WhiteIpAddressControlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }


        // Middleware'ler iç içe çalışır. Bir middleware'in içinde başka bir middleware'i çağırmak için _requestDelegate.Invoke(context) veya kısaca _requestDelegate(context) kullanılır. Bu, mevcut middleware'in işlemlerini tamamladıktan sonra sonraki middleware'e geçmesini sağlar.
        public async Task Invoke(HttpContext context) {

            // ıpv4 localhost: 127.0.0.1        ıpvl ocalhost: ::1


            // İstek yapan kullanıcının IP adresini alır
            var reqIpAddress = context.Connection.RemoteIpAddress;

            // if else gibi bişi gelen  ip ile bizim ip yi karşılatırır doğrysta trure yanlışşsa false değer atar AnyWhiteIpAddress
            bool AnyWhiteIpAddress =  IPAddress.Parse(WhiteIpAddress).Equals(reqIpAddress);


            if (AnyWhiteIpAddress == true) { 
            
            await _requestDelegate(context); // Eğer IP adresi beyaz listede ise, isteği bir sonraki middleware'e iletir
            }
            else
            {
                context.Response.StatusCode = HttpStatusCode.Forbidden.GetHashCode(); 

                await context.Response.WriteAsync("Forbidden" + WhiteIpAddress); 
            }

        }

        //middeleware i aktif hale getirmek için program.cs deki configure metoduna eklememiz gerekiyor  app.UseMiddleware<WhiteIpAddressControlMiddleware>(); şeklinde ekleyebiliriz



    }
}
