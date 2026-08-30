using HastaneMVC.Data;
using HastaneMVC.DTOs;
using ImTools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
namespace HastaneMVC.Controllers
{
    public class HomeeeController : Controller
    {

        // sunucuda 
        readonly IConfiguration configuration;  

        public HomeeeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Indexxx()
        {  // secret.json'dan veri okuma  için    


            var kullanici = configuration["MailBilgileri:Kullanici"];

            var sifre = configuration["MailBilgileri:Sifre"];   


            return Content("Debug için buraya bak");
        }



        // uygulama sunuucuda ayağa kalktı  sunucuda environment değişkenlerin var senin onlar kullanılır


        // Environment 

        //  gelişme asaması sunucuda ayağa kalktığı aşamalarda envirınment değişkenleri bulunur  

        // I- development    II- staging   III- production    bir tane veri tabanımız var   bunu productionda kullanabilirz   ama developmentde kullanamayız  bunu ayarlarız
        // kod hangi ortamda çalışıyorsa ona göre environment ile yönlendirme yapılır



        //

        // state belirtmek console yazdırmak için


        protected readonly HastaneContext _context;

        public HomeeeController(HastaneContext context)
        {
            _context = context;
        }



        


    }
}
