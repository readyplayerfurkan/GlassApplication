using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GlassApplication.Models;
using GlassApplication.Models.Abstract;

namespace GlassApplication.Controllers;

public class ÜrünListeleController : Controller
{
    private readonly ILogger<ÜrünListeleController> _logger;
    private readonly IÜrünService _ürünService;
    private readonly IÜrünRepository _ürünRepository;

    public ÜrünListeleController(ILogger<ÜrünListeleController> logger, IÜrünService ürünService, IÜrünRepository ürünRepository)
    {
        _logger = logger;
        _ürünService = ürünService;
        _ürünRepository = ürünRepository;
    }

    [HttpPost]
    public IActionResult FirmaSeç(string firmaKodu)
    {
        if (!string.IsNullOrEmpty(firmaKodu) && firmaKodu != "empty")
        {
            HttpContext.Session.SetString("SeçilenFirma", firmaKodu);
        }

        // Örneðin redirect ile kullanýcýyý ürün listesine yönlendirebilirsin:
        return RedirectToAction("Index", "ÜrünListele", new { category = "All" });
    }


    public IActionResult Index(string category)
    {
        ViewData["ShowProcessNavbar"] = false;

        if(category != "FromSearch")
            _ürünService.ListByCategory(category);

        return View(_ürünRepository.GetAll());
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
