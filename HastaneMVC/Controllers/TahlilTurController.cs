using HastaneMVC.Data;
using HastaneMVC.DTOs;
using HastaneMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneMVC.Controllers
{
    public class TahlilTurController : Controller
    {

        private readonly HastaneContext _context;


        public  TahlilTurController( HastaneContext context)
        {

            _context =context;
        }


        public IActionResult Index()
        {


            IEnumerable<TahlilTurModelDTO> tahlilturDTO = _context.TahlilTurleri.Where(r => r.IsDeleted == false).Select(i => new TahlilTurModelDTO
            {
                Id = i.Id,
                Ad = i.Ad,


            });


            /*          IEnumerable kullanıldığında .ToList() zorunlu değildir. Ancak List kullanılıyorsa .ToList() gereklidir.
             *          
             *          
             *          
             *          yukarıdaki alternatif kullanım toList() ile listeleri atmak yerine başta liste olarak ayarlıyoruz
                        var tahlilturDTO=_context.TahlilTurleri.Where(r=>r.IsDeleted==false).Select(i=> new TahlilTurModelDTO
                        {
                            Id = i.Id,
                            Ad=i.Ad,


                        } ).ToList();
            */

            return View(tahlilturDTO);

         
        }


        [HttpGet]
        public IActionResult Ekle()
        {
            return View();  
        }


        [HttpPost]
        public IActionResult Ekle(TahlilTurModelDTO modelDTO)
        {
            
            if (ModelState.IsValid)
            {
                var entity = new TahlilTurModel
                {
                    Ad = modelDTO.Ad,
                    Id = modelDTO.Id,
                };



                _context.TahlilTurleri.Add(entity);
                _context.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(modelDTO);

        }

       

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var tahliltur = _context.TahlilTurleri.Find(id);
            var dto = new TahlilTurModelDTO
            {
                Id = tahliltur.Id,
                Ad = tahliltur.Ad,
            };
            return View(dto);
        }


        [HttpPost]
        public IActionResult Guncelle(TahlilTurModelDTO modelDTO)
        {





            if (ModelState.IsValid)
            {
                var eskitahlil = _context.TahlilTurleri.Find(modelDTO.Id);
                eskitahlil.Ad = modelDTO.Ad;

                _context.SaveChanges();
                return RedirectToAction("Index");


            }


            return View(modelDTO);

        }



        [HttpGet]
        public IActionResult Sil(int id) {

            var tahliltur = _context.TahlilTurleri.Find(id);
            var dto = new TahlilTurModelDTO
            {
                Id = tahliltur.Id,
                Ad = tahliltur.Ad,
            };
            return View(dto);
        }



        [HttpPost, ActionName("Sil")]
        public IActionResult silonaylnadı(int id)
        {
            var tahliltur = _context.TahlilTurleri.Find(id);

            tahliltur.IsDeleted=true;
            tahliltur.DeleteTime=DateTime.Now;
           // _context.TahlilTurleri.Remove(tahliltur);
            _context.SaveChanges();



            return RedirectToAction("Index");

        }
    }
}
