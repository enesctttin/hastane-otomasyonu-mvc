using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Include işlemleri için (Çok Önemli!)
using HastaneMVC.Data;
using HastaneMVC.Models;
using HastaneMVC.DTOs;
using HastaneMVC.ViewModels;
using System.Linq;
using HastaneMVC.Services;

namespace HastaneMVC.Controllers
{
    public class RandevuController : BaseController
    {
        public RandevuController(HastaneContext context, RandevuService randevuService) : base(context, randevuService)
        {
        }


        public IActionResult Index()
        {
            var randevuListesi = _context.Randevular.Where(i=>i.IsDeleted==false)
                .Select(r=> new RandevuModelDTO
                {
                    Id=r.Id,
                    TarihSaat=r.TarihSaat,

                    HastaAdSoyad=r.Hasta.AdSoyad,
                    DoktorAdSoyad=r.Doktor.AdSoyad,
                    BransAdi=r.Doktor.Brans.BransAdi,

                }).ToList();
         

            return View(randevuListesi);
        }

        [HttpGet]
        public IActionResult Ekle()
        {
            var viewModel = new RandevuEkleVM
            {
                YeniRandevu = new RandevuModelDTO(),

                DoktorListesi =doktorgetir(),

                HastaListesi = hastalarigetir(),

                BransListesi =branslarigetir()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Ekle(RandevuEkleVM model)
        {

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("HATA: " + error.ErrorMessage);
            }



            if (!ModelState.IsValid)
            {
                model.DoktorListesi = doktorgetir();
                model.HastaListesi = hastalarigetir();
                model.BransListesi = branslarigetir();
                return View(model);
            }

            var entity = new RandevuModel
            {
                HastaId = model.YeniRandevu.HastaId,
                DoktorId = model.YeniRandevu.DoktorId,
                TarihSaat = model.YeniRandevu.TarihSaat,
                DoktorNotu = string.IsNullOrEmpty(model.YeniRandevu.DoktorNotu)
                             ? "Henüz not girilmedi."
                             : model.YeniRandevu.DoktorNotu
            };

            _context.Randevular.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            //var randevu = _context.Randevular.Find(id);

            var randevu = _context.Randevular
         .Where(i => i.Id == id)
         .Select(r => new RandevuModelDTO
         {
             Id = r.Id,
             HastaId = r.HastaId,
             DoktorId = r.DoktorId,
             TarihSaat = r.TarihSaat,
             DoktorNotu = r.DoktorNotu,
             BransId = r.Doktor.BransId
         })
         .FirstOrDefault();

            var viewModel = new RandevuEkleVM
            {
                YeniRandevu = randevu,
                DoktorListesi = doktorgetir(),
                HastaListesi = hastalarigetir(),
                BransListesi = branslarigetir(),
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Guncelle(RandevuEkleVM model)
        {
            if (!ModelState.IsValid)
            {
                model.DoktorListesi = doktorgetir();
                model.HastaListesi = hastalarigetir();
                model.BransListesi = branslarigetir();
                return View(model);
            }

            var eskiRandevu = _context.Randevular.Find(model.YeniRandevu.Id);

            eskiRandevu.TarihSaat = model.YeniRandevu.TarihSaat;
            eskiRandevu.HastaId = model.YeniRandevu.HastaId;
            eskiRandevu.DoktorId = model.YeniRandevu.DoktorId;
            eskiRandevu.DoktorNotu = model.YeniRandevu.DoktorNotu;

            _context.SaveChanges();
            return RedirectToAction("Index");


        }

        [HttpGet]
        public IActionResult Sil(int id)
        {
            var randevu = _randevuService.RandevuGetir(id);
            var model = new RandevuSilVM
            {
                randevu = randevu,
            };
            return View(model); 
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilOnay(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                randevu.DeleteTime = DateTime.Now;
                randevu.IsDeleted= true;
              //  _context.Randevular.Remove(randevu);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public JsonResult DoktorlariGetir(int bransId)
        {
            var doktorlar = _context.Doktorlar
                .Where(d => d.BransId == bransId && d.IsDeleted==false)
                .Select(d => new
                {
                    id = d.Id,
                    adSoyad = d.AdSoyad
                })
                .ToList();

            return Json(doktorlar);
        }

        private IEnumerable<BransModelDTO> branslarigetir()
        {
            return _context.Branslar.Where(b => b.IsDeleted == false).Select(c => new BransModelDTO
            {
                Id = c.Id,
                BransAdi = c.BransAdi,

            }).ToList();
        }


        private IEnumerable<HastaModelDTO> hastalarigetir()
        {
            return _context.Hastalar.Where(b => b.IsDeleted == false).Select(c => new HastaModelDTO
            {
                AdSoyad=c.AdSoyad,
                Id=c.Id

            }).ToList();
        }


        private IEnumerable<DoktorModelDTO> doktorgetir()
        {
            return _context.Doktorlar.Where(b => b.IsDeleted == false).Select(c => new DoktorModelDTO
            {
                AdSoyad=c.AdSoyad,
                Id=c.Id
               
            }).ToList();
        }

    }
}
