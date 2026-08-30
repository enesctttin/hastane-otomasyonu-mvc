using dependency_injection.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using dependency_injection.Services;
using dependency_injection.Services.Interfaces;
namespace dependency_injection.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    readonly ILog _log;
    public HomeController(ILogger<HomeController> logger, ILog log)
    {
        _logger = logger;

        _log = log;


    }

    // containerdaki nesneleri constructur ile talep edebiliyoruz  aşağıdaki örnek bu   veya yuakırdaki gibi interface kullanmadanda yapabiliriz bunu




    public IActionResult Index()
    {
        Textlog log = new Textlog();
        log.Log();
        int e=0;
        Consolelog log2 = new Consolelog(e);
        log2.Log();

        _log.Log();

        // controller bazlı değil Action bazlı için  
        return View();



    }

    public IActionResult Index2([FromServices]ILog log)
    {
         log.Log();


        // controller bazlı değil Action bazlı için  
        return View();



    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
