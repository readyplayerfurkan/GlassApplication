namespace GlassApplication.Models
{
    public class SiparişListViewModel
    {
        public List<SiparişModel> Siparişler { get; set; }
        public int ToplamSiparişSayısı { get; set; }
        public int MevcutSayfa { get; set; }
        public int ToplamSayfa { get; set; }

        public SiparisFilter Filtre { get; set; }
    }
}