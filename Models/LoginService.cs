using GlassApplication.Models.Abstract;
using GlassApplication.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GlassApplication.Models
{
    public class LoginService : ILoginService
    {
        private readonly GW_SISTEM _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor'da HttpContextAccessor'ı enjekte ediyoruz
        public LoginService(GW_SISTEM context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Login(string username, string password)
        {
            var user = await _context.KULLANICI
                .FirstOrDefaultAsync(k => k.KOD == username && k.SIFRE == password);

            if (user != null)
            {
                // Giriş başarılıysa, kullanıcının giriş yapabildiği firmaları al
                var firmalar = await GetAllFirma(user);

                // Kullanıcı bilgilerini session'a kaydet
                _httpContextAccessor.HttpContext.Session.SetString("KullaniciAdi", user.ADI + " " + user.SOYADI);
                _httpContextAccessor.HttpContext.Session.SetString("KullaniciId", user.ID.ToString());

                // Firmaları JSON olarak session'a kaydet
                var firmalarJson = JsonSerializer.Serialize(firmalar);
                _httpContextAccessor.HttpContext.Session.SetString("Firmalar", firmalarJson);

                return true;
            }

            return false;
        }
        public async Task<List<string>> GetAllFirma(Kullanici user)
        {
            var firmalar = await (
                from k in _context.KULLANICI
                join m in _context.KULLANICI_YETKI on k.ID equals m.KULLANICI_ID
                join f in _context.FIRMA on m.ISLEM_NO equals f.ID
                where m.ISLEM_ADI == "Firma Yetki"
                      && m.KULLANICI_ID == user.ID
                      && f.ID >= 1
                select f.ADI
            ).Distinct().ToListAsync();

            return firmalar;
        }
    }
}
