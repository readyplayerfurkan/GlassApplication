namespace GlassApplication.Models
{
    public class ÜrünListViewModel
    {
        public List<ÜrünModel> Ürünler { get; set; }
        public double ToplamÜrünSayısı { get; set; }
        public int MevcutSayfa { get; set; }
        public int ToplamSayfa { get; set; }
        public SiparisFilterModel Filtre { get; set; }
    }
}