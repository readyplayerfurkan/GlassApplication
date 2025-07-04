using GlassApplication.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace GlassApplication.Models;

public class CariService
{
    private readonly AuthorizationDatabase _authorizationDatabase;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CariService(AuthorizationDatabase authorizationDatabase, IHttpContextAccessor httpContextAccessor)
    {
        _authorizationDatabase = authorizationDatabase;
        _httpContextAccessor = httpContextAccessor;       
    }
    
    public async Task<List<Cari>> GetCariListAsync()
    {
        // Kullanıcı ID'sini session'dan al
        string kullaniciID = _httpContextAccessor.HttpContext.Session.GetString("KullaniciId");

        if (!int.TryParse(kullaniciID, out int parsedKullaniciId))
            return new List<Cari>(); // geçersiz ID varsa boş liste döndür

        // Veritabanı sorgusu
        var cariList = await _authorizationDatabase.KULLANICI_YETKI
            .Where(ky => ky.ISLEM_ADI == "Cari Yetki" && ky.KULLANICI_ID == parsedKullaniciId)
            .Select(ky => new Cari
            {
                Cari_ID = ky.ISLEM_FIRMA_ID,
                Firma_ID = ky.ISLEM_NO,
                Cari_Adi = ky.OZEL_KOD
            })
            .ToListAsync();

        return cariList;
    }
}