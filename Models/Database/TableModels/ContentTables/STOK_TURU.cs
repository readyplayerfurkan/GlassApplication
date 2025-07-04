using System.ComponentModel.DataAnnotations.Schema;

namespace GlassApplication.Models.Database.DatabaseModels
{
    [Table("STOK_TURU", Schema = "dbo")]
    public class STOK_TURU
    {
        public int? ID { get; set; }
        public string ADI { get; set; }

        // Navigation
        public virtual ICollection<STOK> Stoklar { get; set; }
    }
}
