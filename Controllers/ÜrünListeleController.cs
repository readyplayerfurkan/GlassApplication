using Microsoft.AspNetCore.Mvc;
using GlassApplication.Models;
using GlassApplication.Models.Abstract;

namespace GlassApplication.Controllers;

public class ÜrünListeleController : Controller
{
    private readonly IÜrünRepository _ürünRepository;

    public ÜrünListeleController(IÜrünRepository ürünRepository)
    {
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
        return RedirectToAction("Index", "ÜrünListele", new { category = "Tek Cam" });
    }


    public IActionResult Index(string category, string searchTerm, bool IsItSearched, int sayfa = 1)
    {
        ViewData["ShowProcessNavbar"] = false;
        ViewData["ShowCategoryNavbar"] = true;
        ViewData["category"] = category;

        int toplamÜrünSayýsý = _ürünRepository.GetAll(category, searchTerm, IsItSearched).Count;
            
        var ürünler = _ürünRepository.GetAll(category, searchTerm, IsItSearched)
            .Skip((sayfa - 1) * 9)
            .Take(9)
            .ToList();

        var model = new ÜrünListViewModel
        {
            Ürünler = ürünler,
            ToplamÜrünSayýsý = toplamÜrünSayýsý,
            MevcutSayfa = sayfa,
            ToplamSayfa = (int)Math.Ceiling((double)toplamÜrünSayýsý / 9)
        };

        return View(model);
    }
}
