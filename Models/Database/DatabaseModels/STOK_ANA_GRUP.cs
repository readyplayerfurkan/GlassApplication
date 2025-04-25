using System.ComponentModel.DataAnnotations.Schema;

namespace GlassApplication.Models.Database.DatabaseModels
{
    [Table("STOK_ANA_GRUP", Schema = "dbo")]
    public class STOK_ANA_GRUP
    {
        public int id { get; set; }
        public string adi { get; set; }

        public virtual ICollection<STOK> Stoklar { get; set; }
    }

}
