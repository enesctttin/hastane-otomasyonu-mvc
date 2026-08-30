using HastaneMVC.Data;
using HastaneMVC.DTOs;

namespace HastaneMVC.Services
{
    public class BaseService
    {
        protected HastaneContext _context;

        public BaseService(HastaneContext context)
        {
            _context = context;
        }



        protected IEnumerable<BransModelDTO> branslarigetir()
        {
            return _context.Branslar.Where(b => b.IsDeleted == false).Select(c => new BransModelDTO
            {
                Id = c.Id,
                BransAdi = c.BransAdi,

            }).ToList();
        }


        protected IEnumerable<HastaModelDTO> hastalarigetir()
        {
            return _context.Hastalar.Where(b => b.IsDeleted == false).Select(c => new HastaModelDTO
            {
                AdSoyad = c.AdSoyad,
                Id = c.Id

            }).ToList();
        }


        protected IEnumerable<DoktorModelDTO> doktorgetir()
        {
            return _context.Doktorlar.Where(b => b.IsDeleted == false).Select(c => new DoktorModelDTO
            {
                AdSoyad = c.AdSoyad,
                Id = c.Id

            }).ToList();
        }






    }
}
