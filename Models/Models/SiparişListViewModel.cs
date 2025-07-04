using GlassApplication.Models.Database.TableModels.ContentTables;

namespace GlassApplication.Models
{
    public class SiparişListViewModel
    {
        public List<SiparisModel> Siparişler { get; set; }
        public int ToplamSiparişSayısı { get; set; }
        public int MevcutSayfa { get; set; }
        public int ToplamSayfa { get; set; }

        public SiparisFilterModel Filtre { get; set; }
    }
}