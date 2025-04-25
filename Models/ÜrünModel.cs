namespace GlassApplication.Models
{
    public class ÜrünModel
    {
        public string? Kategori { get; set; } // ADI
        public string? Ad { get; set; } // adi
        public int? ID { get; set; } // kod? // cam_id1?
        public string? Açıklama { get; set; } // AADI
        public string? Renk { get; set; } // renk
        public string? Özellik { get; set; } // 
        public float? Kalınlık { get; set; } // cam_kal1
        public string? Kenarİşlemi { get; set; } // ?
        public string? Yüzeyİşlemi { get; set; } // ?
        public string? Isılİşlem { get; set; } // ?
        public string? Laminasyon { get; set; } // ?
        public string? Kapama { get; set; } // ?
        public float? BirimFiyatı { get; set; } // ?
        public float? StokDurumu { get; set; } // ?
        public int? TeslimSüresi { get; set; } // ? 
        public string? MaxÖlçü { get; set; } // ?
        public bool IsItSearched { get; set; } 
    }
}
