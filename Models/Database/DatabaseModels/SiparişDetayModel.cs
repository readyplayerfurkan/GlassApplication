using System.ComponentModel.DataAnnotations;

namespace GlassApplication.Models.Database.DatabaseModels
{
    public class SiparişDetayModel
    {
        [Key]
        public int ID { get; set; }
        public int? sip_id { get; set; }
        public int? sip_det_id { get; set; }
        public string? adi0 { get; set; }
        public string? adi1 { get; set; }
        public string? adi2 { get; set; }
        public string? pvb_renk_kodu { get; set; }
        public double? tutar_net { get; set; }
    }

}
