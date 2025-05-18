using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlassApplication.Models
{
    [Table("vw_SIPARIS_BASLIK_DETAY_PROCESSLI", Schema = "dbo")]
    public class SiparişModel
    {
        [Key]
        public int ID { get; set; }
        public string? cc_giris_kul_adi { get; set; }
        public int? cc_siparis_durumu { get; set; }
        public string? aciklama { get; set; }
        public string? ca_adi { get; set; }
        public string? musteri_adi { get; set; }
        public int sip_no { get; set; } 
        public DateTime teslim_tarih { get; set; }
        public string? siparis_DRM { get; set; }
        public string? adi0 { get; set; }
        public string? adi1 { get; set; }
        public string? adi2 { get; set; }
        public string? pvb_renk_kodu { get; set; }
        public DateTime? termin_tarih { get; set; }
        public double? tutar_net { get; set; }
        public string? firma { get; set; }
        public int? top_uretilecek_adet { get; set; }
        public int? top_uretilen_adet { get; set; }
        public int? top_uretim_kalan_adet { get; set; }

        // Sipariş Detayları için eklenen sütunlar
        public int? sip_id { get; set; } // NotMapped kaldırıldı

        public int? sip_detay_id { get; set; } // NotMapped kaldırıldı
        public bool IsItSearched { get; set; }
    }
}
