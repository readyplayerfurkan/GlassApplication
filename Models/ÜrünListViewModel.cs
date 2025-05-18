namespace GlassApplication.Models
{
    internal class ÜrünListViewModel
    {
        public List<ÜrünModel> Ürünler { get; set; }
        public double ToplamÜrünSayısı { get; set; }
        public int MevcutSayfa { get; set; }
        public int ToplamSayfa { get; set; }
        public SiparisFilter Filtre { get; set; }
    }
}