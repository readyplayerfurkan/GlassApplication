using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GlassApplication.Models;
using GlassApplication.Models.Abstract;

namespace GlassApplication.Controllers;

public class �r�nListeleController : Controller
{
    private readonly ILogger<�r�nListeleController> _logger;
    private readonly I�r�nService _�r�nService;
    private readonly I�r�nRepository _�r�nRepository;

    public �r�nListeleController(ILogger<�r�nListeleController> logger, I�r�nService �r�nService, I�r�nRepository �r�nRepository)
    {
        _logger = logger;
        _�r�nService = �r�nService;
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
        return RedirectToAction("Index", "�r�nListele", new { category = "All" });
    }


    public IActionResult Index(string category)
    {
        ViewData["ShowProcessNavbar"] = false;

        if(category != "FromSearch")
            _�r�nService.ListByCategory(category);

        return View(_�r�nRepository.GetAll());
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
