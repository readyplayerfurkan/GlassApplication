namespace GlassApplication.Models.Database.TableModels.ContentTables;

public class SiparisModel
{
    public int ID { get; set; }
    public int cari_id { get; set; }
    public string? cc_giris_kul_adi { get; set; }
    public int? cc_siparis_durumu { get; set; }
    public string? aciklama { get; set; }
    public string? ca_adi { get; set; }
    public string? musteri_adi { get; set; }
    public int sip_no { get; set; } 
    public DateTime teslim_tarih { get; set; }
    public string? siparis_DRM { get; set; }
        
    public DateTime? termin_tarih { get; set; }
    public double? tutar_net { get; set; }
    public string? firma { get; set; }
    public int? top_uretilecek_adet { get; set; }
    public int? top_uretilen_adet { get; set; }
    public int? top_uretim_kalan_adet { get; set; }
    
    public int? sip_id { get; set; } 

    public int? sip_detay_id { get; set; } 
    public bool IsItSearched { get; set; }
}