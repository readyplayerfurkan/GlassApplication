using System.Text.Json;
using GlassApplication.Models.Abstract;
using GlassApplication.Models.Database;
using GlassApplication.Models.Database.TableModels.ContentTables;
using Microsoft.EntityFrameworkCore;

namespace GlassApplication.Models
{
    public class SiparişRepository : ISiparişRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private string? UserName = null;
        private string? Firma = null;

        public SiparişRepository(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<List<SiparisModel>> GetSiparişlerAsync(string siparişDurumu)
        {
            UserName = _httpContextAccessor.HttpContext.Session.GetString("KullaniciAdi");
            Firma = _httpContextAccessor.HttpContext.Session.GetString("SeçilenFirma");
            
            var firmalarJson = _httpContextAccessor.HttpContext.Session.GetString("Firmalar");
            var firmalar = JsonSerializer.Deserialize<List<Firma>>(firmalarJson);
            var firmaId = _httpContextAccessor.HttpContext.Session.GetInt32("SeçilenFirmaID");
            
            var seciliFirma = firmalar.Find(x => x.ID == firmaId);
           
            var cariId = _httpContextAccessor.HttpContext.Session.GetInt32("SeciliCariId");
            var baseConnection = _configuration.GetConnectionString("ContentDatabaseConnection");
            var connectionString = $"{baseConnection};Database={seciliFirma.KOD}";

            var options = new DbContextOptionsBuilder<ContentDatabase>()
                .UseSqlServer(connectionString)
                .Options;
            
            using var _dbContext = new ContentDatabase(options);

            var siparislerRaw = await _dbContext.vw_SIPARIS_BASLIK_DETAY_PROCESSLI
                .Where(sd =>
                    sd.siparis_DRM == siparişDurumu &&
                    sd.cari_id == cariId)
                .ToListAsync();

            var siparisler = siparislerRaw
                .Select(s => new SiparisModel
                {
                    cc_giris_kul_adi = s.cc_giris_kul_adi,
                    cc_siparis_durumu = s.cc_siparis_durumu,
                    aciklama = s.aciklama,
                    siparis_DRM = s.siparis_DRM,
                    ca_adi = s.ca_adi,
                    musteri_adi = s.musteri_adi,
                    teslim_tarih = s.teslim_tarih,
                    sip_no = s.sip_no,
                    termin_tarih = s.termin_tarih,
                    tutar_net = s.tutar_net,
                    top_uretilecek_adet = s.top_uretilecek_adet,
                    top_uretilen_adet = s.top_uretilen_adet,
                    top_uretim_kalan_adet = s.top_uretim_kalan_adet,
                    sip_id = s.sip_id,
                    sip_detay_id = s.sip_detay_id,
                    IsItSearched = true,
                    firma = seciliFirma.ADI
                })
                .ToList();

            return siparisler;
        }
    }
}
