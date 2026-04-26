using HastaneMVC.Data;
using HastaneMVC.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneMVC.ViewModels;
using HastaneMVC.DTOs;
using HastaneMVC.Models;

namespace HastaneMVC.Controllers
{
    public class DoktorController : Controller
    {
        private readonly HastaneContext _context;

        public DoktorController(HastaneContext context)
        {
            _context = context;
        }
        public IActionResult Index( string ara)
        {
            List<DoktorModel> doktorlistesi;

            if (ara != null)
            {
                 doktorlistesi = _context.Doktorlar.Where(d => d.Brans.BransAdi==ara && d.IsDeleted==false).Select(d => new DoktorModel
                {
                    Id = d.Id,
                    AdSoyad = d.AdSoyad,
                    BransId = d.BransId,
                    Brans = d.Brans

                }).OrderBy(i=> i.AdSoyad).ToList();
            }
            else
            {
                  doktorlistesi = _context.Doktorlar.Where(d => d.IsDeleted == false).Select(d => new DoktorModel
                {
                    Id = d.Id,
                    AdSoyad = d.AdSoyad,
                    BransId = d.BransId,
                    Brans = d.Brans
                }).ToList();
            }
            return View(doktorlistesi);
        }

        [HttpGet]
        public IActionResult Ekle()
        {
           




            var model = new DoktorEkleVM
            {
                BransListesi = branslarigetir(),
                YeniDoktor = new DoktorModelDTO() // Ekrana boş bir doktor form alanı gönde
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Ekle(DoktorEkleVM gelenKutu)
        {


            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }



            if (!ModelState.IsValid)
            {
                gelenKutu.BransListesi = branslarigetir();
                return View(gelenKutu);
            }

            var entity = new DoktorModel
            {
                AdSoyad = gelenKutu.YeniDoktor.AdSoyad,
                BransId = gelenKutu.YeniDoktor.BransId
            };

            _context.Doktorlar.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");



        }
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var guncellemeDoktor = _context.Doktorlar.Find(id); 
            if (guncellemeDoktor == null)
            {
                return NotFound();
            }
            var model = new DoktorEkleVM
            {
                YeniDoktor = new DoktorModelDTO
                {
                    Id = guncellemeDoktor.Id,
                    AdSoyad = guncellemeDoktor.AdSoyad,
                    BransId = guncellemeDoktor.BransId
                },
                BransListesi = branslarigetir()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Guncelle(DoktorEkleVM model) 
        {
            if (!ModelState.IsValid)
            {
                model.BransListesi = branslarigetir();
                return View(model);
            }

            var eskiDoktor = _context.Doktorlar.Find(model.YeniDoktor.Id);
            eskiDoktor.AdSoyad = model.YeniDoktor.AdSoyad;
            eskiDoktor.BransId = model.YeniDoktor.BransId;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        

        [HttpGet]
        public IActionResult Sil(int id)
        {
            var doktor = _context.Doktorlar.Find(id);
            if (doktor == null)
            {
                return NotFound();
            }
            return View(doktor);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult silonaylandi(int id)
        {
            var doktor = _context.Doktorlar.Find(id);
            if (doktor != null)
            {
                doktor.DeleteTime = DateTime.Now;
                doktor.IsDeleted=true;
               // _context.Doktorlar.Remove(doktor);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // "private" demek: Bu metodu internetteki kimse URL'den çağıramaz, 
        // sadece bu Controller'ın içindeki diğer kodlar kullanabilir.

          
        private  IEnumerable<BransModelDTO> branslarigetir()
        {
            return _context.Branslar.Where(b => b.IsDeleted == false).Select(c => new BransModelDTO
            {
                Id=c.Id,
                BransAdi=c.BransAdi,
                    
            

            }).ToList();

            
        }

    }
}
