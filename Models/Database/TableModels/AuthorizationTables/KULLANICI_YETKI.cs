using System.ComponentModel.DataAnnotations;

namespace GlassApplication.Models.Database
{
    public class KULLANICI_YETKI
    {
        [Key]
        public int KULLANICI_ID { get; set; }

        public int ISLEM_NO { get; set; }

        public string ISLEM_ADI { get; set; }

        public int ISLEM_FIRMA_ID { get; set; }  

        public string OZEL_KOD { get; set; }    
    }
}
