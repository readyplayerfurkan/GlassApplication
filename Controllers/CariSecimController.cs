using Microsoft.AspNetCore.Mvc;

namespace GlassApplication.Controllers;

public class CariSecimController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CariSecimController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    [HttpPost]
    public IActionResult Index(int secilenCariID)
    {
        _httpContextAccessor.HttpContext.Session.SetInt32("SeciliCariId", secilenCariID);
        
        return RedirectToAction("Index", "ÜrünListele", new { category = "TEK CAM" });
    }
}