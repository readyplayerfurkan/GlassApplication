using GlassApplication.Models.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using GlassApplication.Models;

namespace GlassApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginController(ILoginService loginService, IHttpContextAccessor httpContextAccessor)
        {
            _loginService = loginService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            ViewData["ShowNavbar"] = false;
            ViewData["ShowFooter"] = false;
            ViewData["ShowProcessNavbar"] = false;
            ViewData["ShowCategoryNavbar"] = false;
            return View();
        }

        public async Task<IActionResult> KullaniciyiKontrolEt(string username, string password)
        {
            bool isLoginSuccessfull = await _loginService.Login(username, password);

            if (isLoginSuccessfull)
            {
                // Giriş başarılıysa, firmaları getir (bu kısmı servis ya da sabit listeyle yapabilirsin)
                var firmalarJson = _httpContextAccessor.HttpContext.Session.GetString("Firmalar");
                if (!string.IsNullOrEmpty(firmalarJson))
                {
                    var firmalar = JsonSerializer.Deserialize<List<Firma>>(firmalarJson);
                    ViewBag.Firmalar = firmalar;
                }

                TempData["GirişYapıldıMı"] = "true";
                TempData["FirmaSecimi"] = "true";
                TempData["KullaniciAdi"] = username; // Gerekirse sonra kullanmak için saklıyoruz
                ViewData["ShowNavbar"] = false;
                ViewData["ShowFooter"] = false;
                ViewData["ShowProcessNavbar"] = false;
                ViewData["ShowCategoryNavbar"] = false;
                return View("Index"); // Login view'ini tekrar yükle
            }
            else
            {
                TempData["HataMesaji"] = "Kullanıcı adı veya şifre hatalı.";
                return RedirectToAction("Index", "Login");
            }
        }

    }
}
