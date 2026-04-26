using Microsoft.AspNetCore.Mvc;
using HastaneMVC.Data;
using HastaneMVC.DTOs;
using HastaneMVC.Models;
using System.Linq;
namespace HastaneMVC.Controllers
{
    public class HastaController : Controller
    {   
        private readonly HastaneContext _context;
        public HastaController(HastaneContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var HastalistesiDTO =_context.Hastalar.Where(i=>i.IsDeleted==false).Select(i=> new HastaModelDTO
            {
                Id = i.Id,
                AdSoyad = i.AdSoyad,
                TcNo = i.TcNo,  

            }).ToList();                
            return View(HastalistesiDTO); 
        }
        //
        [HttpGet]
        public IActionResult Ekle( )
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(HastaModelDTO yeniHasta )
        {
       

            if (ModelState.IsValid)
            {


                var entity = new HastaModel
                {
                    AdSoyad = yeniHasta.AdSoyad,
                    TcNo = yeniHasta.TcNo,


                };


                _context.Hastalar.Add(entity);
                _context.SaveChanges();



                return RedirectToAction("Index");

            }

            return View(yeniHasta);

        }
       
        //
        [HttpGet]
        public IActionResult guncelle(int id)
        {
            var hasta=_context.Hastalar.Find(id);

            var dto = new HastaModelDTO
            {
                Id=hasta.Id,
                AdSoyad=hasta.AdSoyad,
                TcNo=hasta.TcNo,
            };

            return View(dto);
        }
        [HttpPost]
        public IActionResult guncelle(HastaModelDTO guncelhasta)
        {


            if (ModelState.IsValid)
            {
                var eskiHasta = _context.Hastalar.Find(guncelhasta.Id);

                eskiHasta.TcNo = guncelhasta.TcNo;
                eskiHasta.AdSoyad = guncelhasta.AdSoyad;
                eskiHasta.Id = guncelhasta.Id;

                _context.Hastalar.Update(eskiHasta);
                _context.SaveChanges();
                return RedirectToAction("Index");


            }

            return View(guncelhasta);
        }
        //
        [HttpGet]
        public IActionResult sil(int id)
        {
            var hasta = _context.Hastalar.Find(id);
            if (hasta == null)
            {   
                return NotFound();  
            }

            var dto = new HastaModelDTO
            {
                Id = hasta.Id,
                TcNo = hasta.TcNo,
                AdSoyad = hasta.AdSoyad,
            };


            return View(dto);
        }
        [HttpPost, ActionName("sil")] // Sisteme "Benim dışarıdaki adım 'sil', beni öyle çağırın" diyoruz.
         public IActionResult silonaylandi(int id )
        {
            var hasta = _context.Hastalar.Find(id);
            if (hasta != null)
            {
                hasta.DeleteTime = DateTime.Now;
                hasta.IsDeleted = true;
             //   _context.Hastalar.Remove(hasta);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
