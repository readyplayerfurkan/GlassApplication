using GlassApplication.Models;
using GlassApplication.Models.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace GlassApplication.Controllers
{
    public class SiparişlerListele : Controller
    {
        private readonly ISiparişRepository siparişRepository;
        
        public SiparişlerListele(ISiparişRepository siparişRepository)
        {
            this.siparişRepository = siparişRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string process, [FromQuery] SiparisFilter filter, int sayfa = 1)
        {
            // Filtreyi null ise varsayılan bir değerle başlatıyoruz
            filter ??= new SiparisFilter();

            ViewData["ShowCategoryNavbar"] = false;
            ViewData["ShowProcessNavbar"] = true;
            ViewData["process"] = process;

            // Veritabanından tüm siparişleri çekiyoruz
            var tümSiparişler = await siparişRepository.GetSiparişlerAsync(process);

            // Filtreyi uygula
            var filtrelenmişSiparişler = FilterSiparisler(tümSiparişler, filter);

            // Toplam sipariş sayısını alıyoruz
            var toplamSiparişSayısı = filtrelenmişSiparişler.Count;

            // Sayfalama işlemi
            var siparişler = filtrelenmişSiparişler
                .Skip((sayfa - 1) * 9)
                .Take(9)
                .ToList();

            var model = new SiparişListViewModel
            {
                Siparişler = siparişler,
                ToplamSiparişSayısı = toplamSiparişSayısı,
                MevcutSayfa = sayfa,
                ToplamSayfa = (int)Math.Ceiling((double)toplamSiparişSayısı / 9),
                Filtre = filter // Filtreyi modele dahil ettik
            };

            return View(model);
        }

        public List<SiparişModel> FilterSiparisler(List<SiparişModel> siparişler, SiparisFilter filter)
        {
            var filteredSiparişler = siparişler.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.CaAdi))
            {
                filteredSiparişler = filteredSiparişler.Where(s => s.ca_adi.Contains(filter.CaAdi, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter.MusteriAdi))
            {
                filteredSiparişler = filteredSiparişler.Where(s => s.musteri_adi.Contains(filter.MusteriAdi, StringComparison.OrdinalIgnoreCase));
            }

            if (filter.SiparisId.HasValue)
            {
                filteredSiparişler = filteredSiparişler.Where(s => s.sip_id == filter.SiparisId.Value);
            }

            if (filter.TeslimTarihStart.HasValue)
            {
                filteredSiparişler = filteredSiparişler.Where(s => s.teslim_tarih >= filter.TeslimTarihStart.Value);
            }

            if (filter.TeslimTarihEnd.HasValue)
            {
                filteredSiparişler = filteredSiparişler.Where(s => s.teslim_tarih <= filter.TeslimTarihEnd.Value);
            }

            return filteredSiparişler.ToList();
        }
    }
}
