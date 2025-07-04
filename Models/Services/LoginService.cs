using GlassApplication.Models.Abstract;
using GlassApplication.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GlassApplication.Models
{
    public class LoginService : ILoginService
    {
        private readonly AuthorizationDatabase _authorizationDatabase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public LoginService(AuthorizationDatabase authorizationDatabase, IHttpContextAccessor httpContextAccessor)
        {
            _authorizationDatabase = authorizationDatabase;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Login(string username, string password)
        {
            var user = await _authorizationDatabase.KULLANICI
                .FirstOrDefaultAsync(k => k.KOD == username && k.SIFRE == password);

            if (user == null) return false;

            var firmalar = await GetAllFirma(user);

            _httpContextAccessor.HttpContext.Session.SetString("KullaniciAdi", user.ADI + " " + user.SOYADI);
            _httpContextAccessor.HttpContext.Session.SetString("KullaniciId", user.ID.ToString());
            
            var firmalarJson = JsonSerializer.Serialize(firmalar);
            _httpContextAccessor.HttpContext.Session.SetString("Firmalar", firmalarJson);

            return true;
        }
        public async Task<List<Firma>> GetAllFirma(KULLANICI user)
        {
            var firmalar = await (
                from k in _authorizationDatabase.KULLANICI
                join m in _authorizationDatabase.KULLANICI_YETKI on k.ID equals m.KULLANICI_ID
                join f in _authorizationDatabase.FIRMA on m.ISLEM_NO equals f.ID
                where m.ISLEM_ADI == "Firma Yetki"
                      && m.KULLANICI_ID == user.ID
                      && f.ID >= 1
                select new Firma {ID = f.ID, KOD = f.KOD, ADI = f.ADI}
            ).Distinct().ToListAsync();

            return firmalar;
        }
    }
}
