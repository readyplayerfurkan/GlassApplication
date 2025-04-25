using System.ComponentModel.DataAnnotations;

namespace GlassApplication.Models.Database
{
    public class KullaniciYetki
    {
        [Key]
        public int KULLANICI_ID { get; set; }
        public int ISLEM_NO { get; set; }
        public string ISLEM_ADI { get; set; }
    }
}
