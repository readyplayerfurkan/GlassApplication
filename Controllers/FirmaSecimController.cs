using System.Text.Json;
using GlassApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlassApplication.Controllers;

public class FirmaSecimController : Controller
{
    private readonly CariService _cariService;

    public FirmaSecimController(CariService cariService)
    {
        _cariService = cariService;
    }

    [HttpPost]
    public async Task<IActionResult> FirmaSeç(int secilenFirmaID)
    {
        HttpContext.Session.SetInt32("SeçilenFirmaID", secilenFirmaID);
        var cariList = await _cariService.GetCariListAsync();
        var serializedCariList = JsonSerializer.Serialize(cariList);

        TempData["CariList"] = serializedCariList;
        TempData["GirişYapıldıMı"] = "false";
        TempData["CariSecimi"] = "true";
        TempData["FirmaSecimi"] = "false";
        TempData["CariSecimi"] = "true";
        ViewData["ShowNavbar"] = false;
        ViewData["ShowFooter"] = false;
        ViewData["ShowProcessNavbar"] = false;
        ViewData["ShowCategoryNavbar"] = false;
        return RedirectToAction("Index", "Login");
    }
}