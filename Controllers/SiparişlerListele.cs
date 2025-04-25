using GlassApplication.Models.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GlassApplication.Controllers
{
    public class SiparişlerListele : Controller
    {
        private readonly ISiparişRepository siparişRepository;
        private readonly ISiparişService siparişService;
        
        public SiparişlerListele(ISiparişRepository siparişRepository, ISiparişService siparişService)
        {
            this.siparişRepository = siparişRepository;
            this.siparişService = siparişService;
        }
        public async Task<IActionResult> Index(string process)
        {
            ViewData["ShowCategoryNavbar"] = false;
            ViewData["ShowProcessNavbar"] = true;

            //if(process != "FromSearch")
            //    siparişService.ListByProcess(process);

            var siparişler = await siparişRepository.GetSiparişlerAsync(process);

            return View(siparişler);
        }
    }
}
