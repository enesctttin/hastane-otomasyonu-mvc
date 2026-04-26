using HastaneMVC.Data;
using HastaneMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HastaneMVC.Controllers
{
    public class BaseController : Controller
    {
        protected readonly HastaneContext _context;
        protected readonly RandevuService _randevuService;

        public BaseController(HastaneContext context, RandevuService randevuService)
        {
            _context = context;
            _randevuService = randevuService;
        }

    }
}
