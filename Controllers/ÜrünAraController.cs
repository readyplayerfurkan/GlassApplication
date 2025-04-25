using GlassApplication.Models;
using GlassApplication.Models.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace GlassApplication.Controllers
{
    public class ÜrünAraController : Controller
    {
        private readonly IÜrünService _ürünService;

        public ÜrünAraController(IÜrünService ürünService)
        {
            _ürünService = ürünService;
        }
        
        [HttpPost]
        public IActionResult Index(string searchTerm)
        {
            _ürünService.UpdateSearchStatus(searchTerm);
            return RedirectToAction("Index", "ÜrünListele", new {category = "FromSearch"});
        }

    }
}
