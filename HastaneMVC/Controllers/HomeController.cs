using HastaneMVC.DTOs;
using HastaneMVC.Models;
using HastaneMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;

namespace HastaneMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index1()
             { 
            return View();
        }

        public IActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult createProduct(string bransadi, int id)
        {

            var model = new BransModelDTO
            {
                BransAdi = bransadi,
                Id = id
            };

            // gelen verilerin name değerleri ile oto bind edilir  classdaki ile aynı olacak şekilde yazılırsa otomatik olarak bind edilir

            // veya cshtml sayfasına kullanacağın seyi üste @model namesapacai  modelin     asp-for ile bind edebilirsin oto 

            return View();
        }

        // form üzerinden gelen verileri almak için  view de form oluştur

        public IActionResult FormGetir( IFormCollection datas)
        {
            // formdan gelen verileri datas ile alabiliriz  datas["name"] ile alabiliriz değerleri datas tutar IFromCollection ise bunu sağlar
            var bransadi = datas["bransadi"];
            var id = datas["id"];      // veya (  ) içini  yukarıdaki gibi yazabiliriz

            return View();
        }

        public class model
        {

            public string txtvalue1 { get; set; }

            public string txtvalue2 { get; set; }

            public string txtvalue3 { get; set; }   

        }

        public IActionResult verriial(model aass)  // gelen verileri aass üzeine gelip görebiliriz break point koyup debug yapabiliriz
        {
            


            return View();
        }


        // query string : güvenlik açısından risklidir  url üzerinden veri göndeririz  ? ile başlar  & ile ayırırız  key=value şeklinde olur


        // hızlıdır  isteğin türü ne olursa ona göre çalışır  get veya post  get ile gönderilen veriler url üzerinden gönderilir  post ile gönderilen veriler body üzerinden gönderilir  post daha güvenlidir  get ile gönderilen veriler url üzerinden gönderildiği için güvenlik riski vardır  post ile gönderilen veriler body üzerinden gönderildiği için güvenlik riski yoktur

        public IActionResult QueryStringGetir(string a)
        {
            // query string ile gelen verileri parametre olarak alabiliriz  key=value şeklinde olur  ? ile başlar  & ile ayırırız

            //  Url e /Home/QueryStringGetir?a=deneme şeklinde url üzerinden gönderilir  a=deneme şeklinde gelir  a parametresi ile alabiliriz

            // iki 3 parametreli query string gönderebiliriz  /Home/QueryStringGetir?a=deneme&b=deneme2&c=deneme3 şeklinde url üzerinden gönderilir  a=deneme&b=deneme2&c=deneme3 şeklinde gelir  a,b,c parametreleri ile alabiliriz

            var aaa= Request.QueryString;  // request yapılan endpoint e query string parametresi eklenmiş mi  bununla ilgili bilgi verir

            var avd=Request.Query["a"]; // gelen a yı almanın bir yolu 
            //   user?name=max
            return View();
        }

        //user/max

        // default route pattern: {controller=Home}/{action=Index}/{id?}  id? optional parametre 
        //rotaya uygun parametreler ile id yi karşılayabiliriz  object tipinde int tipinde alabiliriz  id 

        public IActionResult fee(int id)  // object de olabilir string de olabilir
        {
            // karşılamayı request route values iled de yapabiliriz

            var values= Request.RouteValues;  

            return RedirectToAction("Index2"); 
        }

        // view de routue manupule edilebilir   html e şu aşağıdaki yazılırsa 
        // <a asp-action="Index" asp-controller="Home" asp-route-a="ahmet" asp-route-b="mehmet" asp-route-id="123" asp-route-x="egfrhgb">tıkla manipule et</a>


        // header üzerinden veri alma ilgili istekleri diğerleri gibi https den alır   ilgili istekle ilgili nitelikleri barındırır


        public IActionResult headerdencek()
        {

            var headers= Request.Headers;  // to list gelirse daha hızlı erişim olur ne gelmiş gelmemiş anlarız headerlada sadece ingilizce yazı olmallı  key veri tipinin adı value bunun içeriği 


            return View();
        }// castrel sunucusu vizde console açar bütün istekleri karşılayacağım der   postmen kullanılır burada  postmanden istek atılabilir  


        // ajax tabanlı veri alma jquery ile  UI teknolojisi var burada js tabanlı mimarı  
        // ajax clınt tabanlı çalışır0

        public  class AjaxData
            {
            public string A { get; set; }

            public string B { get; set; }
        }

        public IActionResult createproduct()
        {

            //  jquerykutuphanesi kulllanmak sart burada  view e eklenmeli bu https://code.jquery.com/jquery-4.0.0.js
            return View();

        }

        [HttpPost]
        public IActionResult verial(AjaxData gelenveri)
        {

            return View();
        }
        /*  cshtmlde yazılan kısım 
         <script src="https://code.jquery.com/jquery-4.0.0.js"></script>

        <button id="btngonder">gönder</button>



    <script>
        $("btngonder").click(( ) =>
        {

            $.post("https:localhost:5001/product/verial",{a: "a data" , b: "b data"});

        // {a: "a data" , b: "b data"} bu objeyi gönderitoruz back end'e
        }
        )


    </script>

        */
       
        // viewden gelen veriyi burada da neye göre değiştireceksek ona göre çalışır aynı viewiden veri almak için kullanılan iki method farklı dblere kayıt gibi
        public IActionResult abc([Bind(Prefix="item1")]AjaxData aa){

            return View();
            }

        // attribure routing controller bazlı route  

        //  [Route("[controller]/[action]")]  bu en yukarı eklenir   controllerdan miras alan yerin üstüne namespacein altına  home yerine ana yazılırsa ana/index olur yeni url 
        //  [Route("[controller]/[action]")] bu yeni pattern olur program cs tarafı iptal edilir  route ile ilgili olan kısımlar , yerine endpoints.MapController(); eklenir    
        //  [Route("[controller]")]  [Route("[action]")]      parça parça da verilebilir en yukarı   controller  sonra  metodlara göre  action adları doldurulur 


        // custom route handler gelen requeste uygun controller tetiklenir   ve response döner  gelen her request  sadece controller ile karşılanır 
        // belirli istekleri controllerin dısında farklı handle sınıflarında yönlendirsek ve ilgili isteğe göre custumize edip  mvc den çıkıp handle tarzı bir şey olacak 
        // Custom Route Handle : Herhangi bir belirlenmiş route şemasının controller sınıflarından ziyade business mantığında karşılanması ve orada iş group response'un dönülmesi operasyonudur.
        // bu sınıf bir controller değil dosya formatlandırmdayı düşün bunu özel sınıflara dağıtım daha özel hale getirip controllerdan bağımsız bussiness mantığını geliştirmek 
        // bunun için yeni bir proje oluşturuldu  
        // herhangi view render edilecek view açılacak  veri tabanından verileri alıp kullanıcıya sunacaksın bu tarz yerlde bu kullanılmaz genel geçer konularda klasik controller mekanizması kullanılır 
        // gelen isteği controller harici bir sınıfa nasıl göndeririz  rotaları tanımladığımız program.cs de bu işlem yapılacak
        
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
    
   



    }
}
