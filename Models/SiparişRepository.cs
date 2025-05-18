using GlassApplication.Models.Abstract;
using GlassApplication.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace GlassApplication.Models
{
    public class SiparişRepository : ISiparişRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GW_TEST_2025 _gW_TEST_2025;
        private string? UserName = null;
        private string? Firma = null;

        public SiparişRepository(GW_TEST_2025 gW_TEST_2025, IHttpContextAccessor httpContextAccessor)
        {
            _gW_TEST_2025 = gW_TEST_2025;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<SiparişModel>> GetSiparişlerAsync(string siparişDurumu)
        {
            UserName = _httpContextAccessor.HttpContext.Session.GetString("KullaniciAdi");
            Firma = _httpContextAccessor.HttpContext.Session.GetString("SeçilenFirma");

            return await (from sb in _gW_TEST_2025.vw_SIPARIS_BASLIK_DETAY
                          join sd in _gW_TEST_2025.vw_SIPARIS_DETAY
                              on new { sb.sip_id, sb.sip_detay_id } equals new { sip_id = sd.sip_id, sip_detay_id = sd.sip_det_id }
                          where sb.cc_giris_kul_adi == UserName && sb.siparis_DRM == siparişDurumu
                          select new SiparişModel
                          {
                              cc_giris_kul_adi = sb.cc_giris_kul_adi,
                              cc_siparis_durumu = sb.cc_siparis_durumu,
                              aciklama = sb.aciklama,
                              siparis_DRM = sb.siparis_DRM,
                              ca_adi = sb.ca_adi,
                              musteri_adi = sb.musteri_adi,
                              teslim_tarih = sb.teslim_tarih,
                              sip_no = sb.sip_no,
                              adi0 = sd.adi0,
                              adi1 = sd.adi1,
                              adi2 = sd.adi2,
                              pvb_renk_kodu = sd.pvb_renk_kodu,
                              termin_tarih = sb.termin_tarih,
                              tutar_net = sd.tutar_net,
                              top_uretilecek_adet = sb.top_uretilecek_adet,
                              top_uretilen_adet = sb.top_uretilen_adet,
                              top_uretim_kalan_adet = sb.top_uretim_kalan_adet,
                              sip_id = sb.sip_id,
                              sip_detay_id = sb.sip_detay_id,
                              IsItSearched = true,
                              firma = Firma
                          }).ToListAsync();
        }
    }
}

