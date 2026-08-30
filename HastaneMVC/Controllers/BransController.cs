using Microsoft.AspNetCore.Mvc;
using HastaneMVC.Data;   
using HastaneMVC.DTOs; 
using System.Linq;
using HastaneMVC.Models;
using Mono.TextTemplating;
using Microsoft.EntityFrameworkCore;
using LamarCodeGeneration.Util;

namespace HastaneMVC.Controllers
{
    public class BransController : Controller
    {
        // privative Atandıktan sonra bir daha değiştirilemez, bunu sağlar _context e başka yerde atama yapamayız sadece constructor içinde yapılır

        private readonly HastaneContext _context;

        // Dependency Injection (Bağımlılık Enjeksiyonu)
        // ASP.NET sistemi Program.cs'teki ayarları okur
        public BransController(HastaneContext context)   //Constructor Sadece Bir Kere Çalışır
        {
            _context = context;
        }

        





        public IActionResult Index()
        {

            // State denemesi burada, bir metodun içinde
            var u = new DoktorModel(); // DTO değil, entity
            Console.WriteLine(_context.Entry(u).State); // Detached
            // u nesnesi güncellenirse   console modified yazılır 

            var tutucu = new DoktorModel{
               AdSoyad="dfs"
            }
            ;

            u = tutucu;

            Console.WriteLine(_context.Entry(u).State);  // modified 

            // savechange atıp              Console.WriteLine(_context.Entry(u).State);  unchanged oldu 




            var bransListesiDTO = _context.Branslar.Where(i=>i.IsDeleted==false).Select(b=> new BransModelDTO
            {
                Id= b.Id,
                BransAdi=b.BransAdi

            }).ToList();// Veritabanından hepsini çek

         //   var eee=_context.Branslar.ToList();

            return View(bransListesiDTO);  // Ekrana (View'a) gönder
        }


        // create 
        [HttpGet]
        public IActionResult Ekle()
        {
            


            return View();   // Sadece boş formu ekranda gösterir
        }  

        [HttpPost]

        public IActionResult Ekle(BransModelDTO modelDTO)
        {

            /*

            if (ModelState.IsValid)
            {

                //alınan hatayı console yazdırma ya da değişkene atma
                // FirstOrDefault(x=> x.ValidationState==Mircrosoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) eklenebilir
                ViewBag.HataMesaj = ModelState.Values.FirstOrDefault().Errors[0].ErrorMessage;

                // cshtmlde input girilen yerlere span içinde asp-validation for eklenmeli hata mesajı eklemek için  
                return View(new BransModelDTO { });
            var messages = ModelState.Tolist();

            }*/
            // model de verdiğin required de masxlenght veya stringlenght de   html taşınır o 
            // ModelState hataları görmeye ve validasyonu kontrol temeye yarar  

            // cshrtmlde asp-validaton-summary="All"  ile bütün hatalar tek yerde toplanır 0





            if (ModelState.IsValid)
            {
                var entity = new BransModel { BransAdi = modelDTO.BransAdi.ToUpper() };

                _context.Branslar.Add(entity);   // Veritabanına yeni branşı ekle
                _context.SaveChanges();         // Değişiklikleri kaydet

                TempData["success"] = "işlem başarılı";


                return RedirectToAction("Index");  // İşlem bitince liste (Index) sayfasına geri dön

            }

            return View(modelDTO);
        }

        //update
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var brans = _context.Branslar.Find(id); // Düzenlenecek branşı ID'sine göre bul

            var dto = new BransModelDTO
            {
                Id =brans.Id,
                BransAdi = brans.BransAdi,    
            };
            return View(dto); // Formun içine eski bilgileri doldurup ekrana getir

        }

        [HttpPost]
        public IActionResult Guncelle(BransModelDTO guncelBrans)
        {



            if (ModelState.IsValid)
            {


                var eskiBrans = _context.Branslar.Find(guncelBrans.Id);

                eskiBrans.BransAdi = guncelBrans.BransAdi.ToUpper();



                _context.Branslar.Update(eskiBrans); // Üzerinde değişiklik yapılmış halini veritabanında güncelle
                _context.SaveChanges(); // Değişiklikleri kaydet
                TempData["success"] = "işlem başarılı";

                return RedirectToAction("Index"); // Liste sayfasına geri dön
            }
            return View(guncelBrans);



        }

        // SİL

        [HttpGet]
        public IActionResult Sil(int id)
        {
            var brans = _context.Branslar.Find(id); 
            if (brans == null)
            {
                return NotFound(); // Bulamazsa 404 Hata sayfasına gönder
            }

            var dto = new BransModelDTO
            {
                Id = brans.Id,
                BransAdi = brans.BransAdi,
            };


            return View(dto); // Branş bilgilerini onay sayfasına (View) gönder
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilOnaylandi(int id)
        {
            var brans = _context.Branslar.Find(id); 
            if (brans != null)
            {
                brans.DeleteTime = DateTime.Now;
                brans.IsDeleted= true;
                //_context.Branslar.Remove(brans); // Veritabanından sil 

                _context.SaveChanges();
                TempData["success"] = "işlem başarılı";

            }

            return RedirectToAction("Index"); 
        }


    }
}
