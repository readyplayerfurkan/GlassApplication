using Microsoft.AspNetCore.Mvc;
using GlassApplication.Models;
using GlassApplication.Models.Abstract;

namespace GlassApplication.Controllers;

public class �r�nListeleController : Controller
{
    private readonly I�r�nRepository _�r�nRepository;

    public �r�nListeleController(I�r�nRepository �r�nRepository)
    {
        _�r�nRepository = �r�nRepository;
    }

    [HttpPost]
    public IActionResult FirmaSe�(string firmaKodu)
    {
        if (!string.IsNullOrEmpty(firmaKodu) && firmaKodu != "empty")
        {
            HttpContext.Session.SetString("Se�ilenFirma", firmaKodu);
        }

        // �rne�in redirect ile kullan�c�y� �r�n listesine y�nlendirebilirsin:
        return RedirectToAction("Index", "�r�nListele", new { category = "Tek Cam" });
    }


    public IActionResult Index(string category, string searchTerm, bool IsItSearched, int sayfa = 1)
    {
        ViewData["ShowProcessNavbar"] = false;
        ViewData["ShowCategoryNavbar"] = true;
        ViewData["category"] = category;

        int toplam�r�nSay�s� = _�r�nRepository.GetAll(category, searchTerm, IsItSearched).Count;
            
        var �r�nler = _�r�nRepository.GetAll(category, searchTerm, IsItSearched)
            .Skip((sayfa - 1) * 9)
            .Take(9)
            .ToList();

        var model = new �r�nListViewModel
        {
            �r�nler = �r�nler,
            Toplam�r�nSay�s� = toplam�r�nSay�s�,
            MevcutSayfa = sayfa,
            ToplamSayfa = (int)Math.Ceiling((double)toplam�r�nSay�s� / 9)
        };

        return View(model);
    }
}
