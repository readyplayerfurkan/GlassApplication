namespace GlassApplication.Models
{
    public class SiparisFilterModel
    {
        public string? CaAdi { get; set; }
        public string? MusteriAdi { get; set; }
        public int? SiparisId { get; set; }
        public DateTime? TeslimTarihStart { get; set; }
        public DateTime? TeslimTarihEnd { get; set; }

        public SiparisFilterModel() { }
    }

}
