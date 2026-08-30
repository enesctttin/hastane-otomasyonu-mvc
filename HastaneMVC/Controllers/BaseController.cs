using HastaneMVC.Data;
using HastaneMVC.DTOs;
using HastaneMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HastaneMVC.Controllers
{
    public class BaseController : Controller
    {
        /*
        
        protected readonly HastaneContext _context;
        protected readonly RandevuService _randevuService;

        public BaseController(HastaneContext context, RandevuService randevuService)
        {
            _context = context;
            _randevuService = randevuService;
        }
        
        */


        protected readonly HastaneContext _context;

        public BaseController(HastaneContext context)
        {
            _context = context;
        }



        //protected o sınıf ve ondan türeyen sınıflar erişebilir
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
