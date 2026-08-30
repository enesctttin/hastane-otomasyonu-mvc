using HastaneMVC.Data;
using HastaneMVC.DTOs;
using HastaneMVC.DTOs;
using HastaneMVC.Models;
using HastaneMVC.Services;
using HastaneMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HastaneMVC.Controllers
{
    public class DoktorController : BaseController
    {


        private readonly DoktorService _doktorservice ;

        public DoktorController(HastaneContext context, DoktorService doktorservice) : base(context)
        {
            _doktorservice = doktorservice;
        }


        // private readonly HastaneContext _context;
        /*
        public DoktorController(HastaneContext context)
        {
            _context = context;
        }*/

        public IActionResult Index( string ara)
        {
            /*
            ICollection<DoktorModel> doktorlistesi;

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
            */
        //hata vermesin diye    var doktorlistesi=new DoktorModel();

            return View(_doktorservice.indexgetir(ara));
        }

        [HttpGet]
        public IActionResult Ekle()
        {
          
            /*
            var model = new DoktorEkleVM
            {
                BransListesi = branslarigetir(),
                YeniDoktor = new DoktorModelDTO() // Ekrana boş bir doktor form alanı gönde
            };
            return View(model);
            */

            return View(_doktorservice.EkleGet());



        }
        [HttpPost]
        public IActionResult Ekle(DoktorEkleVM gelenKutu)
        {

            // hataları console'a yazdırmak için:
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
                  
/*
            var entity = new DoktorModel
            {
                AdSoyad = gelenKutu.YeniDoktor.AdSoyad,
                BransId = gelenKutu.YeniDoktor.BransId
            };
*/
           var entity= _doktorservice.EklePost(gelenKutu);
            _context.Doktorlar.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");



        }
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            /*
            var guncellemeDoktor = _context.Doktorlar.Find(id); 
         
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

            return View(model);*/


            return View(_doktorservice.GuncelleGet(id));
        }
        [HttpPost]
        public IActionResult Guncelle(DoktorEkleVM model) 
        {
         
            /*
            var eskiDoktor = _context.Doktorlar.Find(model.YeniDoktor.Id);
            eskiDoktor.AdSoyad = model.YeniDoktor.AdSoyad;
            eskiDoktor.BransId = model.YeniDoktor.BransId;
            _context.SaveChanges();  
            */
            _doktorservice.GuncellePost(model);
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

          
        // base serviceden artık halledicez
        /*
        private  IEnumerable<BransModelDTO> branslarigetir()
        {
            return _context.Branslar.Where(b => b.IsDeleted == false).Select(c => new BransModelDTO
            {
                Id=c.Id,
                BransAdi=c.BransAdi,
                    
            

            }).ToList();

            
        }
        */



    }
}
