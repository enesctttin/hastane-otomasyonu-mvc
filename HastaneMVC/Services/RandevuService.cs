using HastaneMVC.Data;
using HastaneMVC.DTOs;
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

