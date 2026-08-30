using HastaneMVC.Data;
using HastaneMVC.DTOs;
using HastaneMVC.Models;
using HastaneMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HastaneMVC.Services
{
    public class RandevuService : BaseService
    {
        public RandevuService(HastaneContext context) : base(context)
        {
        }

        public RandevuModelDTO RandevuGetir(int id)
        {
            var randevu = _context.Randevular.Where(i=> i.Id==id)
                .Select(c => new RandevuModelDTO
                {
                    DoktorAdSoyad = c.Doktor.AdSoyad,
                    BransAdi = c.Doktor.Brans.BransAdi,
                    DoktorId = c.DoktorId,
                    DoktorNotu = c.DoktorNotu,
                    HastaAdSoyad = c.Hasta.AdSoyad,
                    HastaId = c.HastaId,
                    Id = c.Id,
                    TarihSaat = c.TarihSaat
                })
                .FirstOrDefault();


            if (randevu == null) return new RandevuModelDTO();
            return randevu;

        }

        public RandevuModelDTO guncellenecekOlanGetir(int id)
        {


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

       

            return randevu;
        }



        public ICollection <RandevuModelDTO> indexinrandevugetir()
        {

            var randevuListesi = _context.Randevular.Where(i => i.IsDeleted == false)
    .Select(r => new RandevuModelDTO
    {
        Id = r.Id,
        TarihSaat = r.TarihSaat,

        HastaAdSoyad = r.Hasta.AdSoyad,
        DoktorAdSoyad = r.Doktor.AdSoyad,
        BransAdi = r.Doktor.Brans.BransAdi,

    }).ToList();

            return randevuListesi;


        }


        public RandevuEkleVM EkleGetir() {



            var viewModel = new RandevuEkleVM
            {
                YeniRandevu = new RandevuModelDTO(),

                DoktorListesi = doktorgetir(),

                HastaListesi = hastalarigetir(),

                BransListesi = branslarigetir()
            };

            return viewModel;

        }

        public RandevuModel EkleEkle(RandevuEkleVM model)
        {

           if (string.IsNullOrEmpty(model.YeniRandevu.DoktorNotu))
            {
                model.YeniRandevu.DoktorNotu = "Henüz not girilmedi.";
            }

            var entity = new RandevuModel
            {
                HastaId = model.YeniRandevu.HastaId,
                DoktorId = model.YeniRandevu.DoktorId,
                TarihSaat = model.YeniRandevu.TarihSaat,
                DoktorNotu = model.YeniRandevu.DoktorNotu
            };

            return entity;
        }

        public void GuncellePost(RandevuEkleVM model)
        {


            var eskiRandevu = _context.Randevular.Find(model.YeniRandevu.Id);

            eskiRandevu.TarihSaat = model.YeniRandevu.TarihSaat;
            eskiRandevu.HastaId = model.YeniRandevu.HastaId;
            eskiRandevu.DoktorId = model.YeniRandevu.DoktorId;
            eskiRandevu.DoktorNotu = model.YeniRandevu.DoktorNotu;

            _context.SaveChanges();


        }


        public void SilPost(int id)
        {

            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                randevu.DeleteTime = DateTime.Now;
                randevu.IsDeleted = true;
                //  _context.Randevular.Remove(randevu);
                _context.SaveChanges();
            }

        }

  


    }
}












//var randevu = _context.Randevular
//    .Include(r => r.Hasta)
//    .Include(r => r.Doktor).Select(c => new RandevuModelDTO
//    {
//        DoktorAdSoyad = c.Doktor.AdSoyad,
//        BransAdi = c.Doktor.Brans.BransAdi,
//        DoktorId = c.DoktorId,
//        DoktorNotu = c.DoktorNotu,
//        HastaAdSoyad = c.Hasta.AdSoyad,
//        HastaId = c.HastaId,
//        Id = c.Id,
//        TarihSaat = c.TarihSaat
//    })
//    .FirstOrDefault(r => r.Id == id);

