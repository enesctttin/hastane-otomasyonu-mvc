using HastaneMVC.Data;

namespace HastaneMVC.Services
{
    public class BaseService
    {
        protected HastaneContext _context;

        public BaseService(HastaneContext context)
        {
            _context = context;
        }
    }
}
