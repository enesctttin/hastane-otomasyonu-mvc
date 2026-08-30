using HastaneMVC.Data;
using HastaneMVC.DTOs;
using HastaneMVC.Models;
using HastaneMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HastaneMVC.Controllers
{
    public class RandevuTahlilController : Controller
    {
        private readonly HastaneContext _context;

        public RandevuTahlilController(HastaneContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tahlilListesi = _context.RandevuTahlilleri.Where(r=>r.IsDeleted==false)
                .Select(rt => new RandevuTahlilModelDTO
                {
                    Id = rt.Id,
                    RandevuId = rt.RandevuId,
                    TahlilTuruId = rt.TahlilTuruId,
                    Sonuc = rt.Sonuc,

                    // Navigation property'ler üzerinden isimlere uzanıyoruz, sql arkada otomatik JOIN atacak
                    HastaAdSoyad = rt.Randevu.Hasta.AdSoyad,
                    DoktorAdSoyad = rt.Randevu.Doktor.AdSoyad,
                    TahlilAd = rt.TahlilTuru.Ad
                }).ToList();

            return View(tahlilListesi);
        }

        [HttpGet]
        public IActionResult Ekle()


        {
            var viewModel = new RandevuTahlilEkleVM
            {
                YeniRandevuTahlil = new RandevuTahlilModel(),

                TahlilTurListesi = _context.TahlilTurleri
                    .Select(t => new TahlilTurModelDTO { Id = t.Id, Ad = t.Ad })
                    .ToList(),

                RandevuListesi = _context.Randevular
                    .Select(r => new RandevuModelDTO
                    {
                        Id = r.Id,
                        HastaAdSoyad = r.Hasta.AdSoyad + " (Dr: " + r.Doktor.AdSoyad + " - " + r.TarihSaat.ToString("dd.MM.yyyy") + ")"
                    }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Ekle(RandevuTahlilEkleVM model)
        {



      

                // nullable öncesi
                if (string.IsNullOrEmpty(model.YeniRandevuTahlil.Sonuc))
                {
                    model.YeniRandevuTahlil.Sonuc = "Sonuç henüz çıkmadı / Bekleniyor.";
                }

                _context.RandevuTahlilleri.Add(model.YeniRandevuTahlil);

                _context.SaveChanges();

                return RedirectToAction("Index");


        
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var randevuTahlil = _context.RandevuTahlilleri.Find(id);
            if (randevuTahlil == null) return NotFound();

            var viewModel = new RandevuTahlilEkleVM
            {
                YeniRandevuTahlil = randevuTahlil,

                TahlilTurListesi = _context.TahlilTurleri
                    .Select(t => new TahlilTurModelDTO { Id = t.Id, Ad = t.Ad }).ToList(),

                RandevuListesi = _context.Randevular
                    .Select(r => new RandevuModelDTO
                    {
                        Id = r.Id,
                        HastaAdSoyad = r.Hasta.AdSoyad + " (Dr: " + r.Doktor.AdSoyad + " - " + r.TarihSaat.ToString("dd.MM.yyyy") + ")"
                    }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Guncelle(RandevuTahlilEkleVM model)
        {


                var eskiKayit = _context.RandevuTahlilleri.Find(model.YeniRandevuTahlil.Id);

                eskiKayit.RandevuId = model.YeniRandevuTahlil.RandevuId;
                eskiKayit.TahlilTuruId = model.YeniRandevuTahlil.TahlilTuruId;
                eskiKayit.Sonuc = model.YeniRandevuTahlil.Sonuc; // bunu ekle

                _context.SaveChanges();
                return RedirectToAction("Index");
      



        }

        [HttpGet]
        public IActionResult Sil(int id)
        {
            var dto = _context.RandevuTahlilleri
                .Where(rt => rt.Id == id)
                .Select(rt => new RandevuTahlilModelDTO
                {
                    Id = rt.Id,
                    HastaAdSoyad = rt.Randevu.Hasta.AdSoyad,
                    TahlilAd = rt.TahlilTuru.Ad,
                    Sonuc = rt.Sonuc
                }).FirstOrDefault();

            if (dto == null) return NotFound();

            return View(dto); 
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilOnay(int id)
        {
            var tahlil = _context.RandevuTahlilleri.Find(id);
            if (tahlil != null)
            {
                tahlil.IsDeleted = true;
                //   _context.RandevuTahlilleri.Remove(tahlil);
                tahlil.DeleteTime = DateTime.Now;

                _context.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}