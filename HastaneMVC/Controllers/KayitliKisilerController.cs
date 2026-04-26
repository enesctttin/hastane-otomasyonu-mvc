using HastaneMVC.Data;
using HastaneMVC.DTOs;
using HastaneMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;




namespace HastaneMVC.Controllers
{
    public class KayitliKisilerController : Controller
    {
        private readonly HastaneContext _context;

        public KayitliKisilerController(HastaneContext context)
        {

            _context = context;
        }
        





        public IActionResult Index()
        {
            var model = _context.Randevular.Select(i => new RandevuModelDTO
            {
                  DoktorAdSoyad = i.Doktor.AdSoyad,
                  HastaAdSoyad=i.Hasta.AdSoyad,
                  HastaId=i.HastaId,
                  DoktorId=i.DoktorId,
                  IsDeleted=i.IsDeleted,
                  

            }).ToList();
                

            return View(model);
        }
    }
}
