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

    public IActionResult Index(string category, string searchTerm, bool IsItSearched, int sayfa = 1)
    {
        ViewData["ShowProcessNavbar"] = false;
        ViewData["ShowCategoryNavbar"] = true;
        ViewData["category"] = category;

        int toplamÜrünSayısı = _ürünRepository.GetAll(category, searchTerm, IsItSearched).Count;
            
        var ürünler = _ürünRepository.GetAll(category, searchTerm, IsItSearched)
            .Skip((sayfa - 1) * 9)
            .Take(9)
            .ToList();

        var model = new ÜrünListViewModel
        {
            Ürünler = ürünler,
            ToplamÜrünSayısı = toplamÜrünSayısı,
            MevcutSayfa = sayfa,
            ToplamSayfa = (int)Math.Ceiling((double)toplamÜrünSayısı / 9)
        };
        
        return View(model);
    }
}
