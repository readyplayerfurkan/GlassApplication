using System.ComponentModel.DataAnnotations.Schema;

namespace GlassApplication.Models.Database.DatabaseModels
{
    [Table("STOK", Schema ="dbo")]
    public class STOK
    {
        public int Id { get; set; }
        public string? adi { get; set; }
        public short? cam_tek_cift { get; set; }
        public int alt_grup { get; set; }
        public int ana_grup { get; set; }
        public string? renk { get; set; }
        public double? cam_kal1 { get; set; }

        public virtual STOK_TURU StokTuru { get; set; }
        public virtual STOK_ANA_GRUP AnaGrup { get; set; }
        public virtual STOK_ALT_GRUP AltGrup { get; set; }
    }
}
