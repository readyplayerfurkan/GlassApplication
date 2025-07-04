using GlassApplication.Models;
using GlassApplication.Models.Abstract;
using GlassApplication.Models.Database.TableModels.ContentTables;
using Microsoft.AspNetCore.Mvc;

namespace GlassApplication.Controllers
{
    public class SiparişlerListeleController : Controller
    {
        private readonly ISiparişRepository siparişRepository;
        
        public SiparişlerListeleController(ISiparişRepository siparişRepository)
        {
            this.siparişRepository = siparişRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string process, [FromQuery] SiparisFilterModel filterModel, int sayfa = 1)
        {
            // Filtreyi null ise varsayılan bir değerle başlatıyoruz
            filterModel ??= new SiparisFilterModel();

            ViewData["ShowCategoryNavbar"] = false;
            ViewData["ShowProcessNavbar"] = true;
            ViewData["process"] = process;

            // Veritabanından tüm siparişleri çekiyoruz
            var tümSiparişler = await siparişRepository.GetSiparişlerAsync(process);

            // Filtreyi uygula
            var filtrelenmişSiparişler = FilterSiparisler(tümSiparişler, filterModel);

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
                Filtre = filterModel // Filtreyi modele dahil ettik
            };

            return View(model);
        }

        public List<SiparisModel> FilterSiparisler(List<SiparisModel> siparişler, SiparisFilterModel filterModel)
        {
            var filteredSiparişler = siparişler.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterModel.CaAdi))
            {
                filteredSiparişler = filteredSiparişler.Where(s => s.ca_adi.Contains(filterModel.CaAdi, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filterModel.MusteriAdi))
            {
                filteredSiparişler = filteredSiparişler.Where(s => s.musteri_adi.Contains(filterModel.MusteriAdi, StringComparison.OrdinalIgnoreCase));
            }

            if (filterModel.SiparisId.HasValue)
            {
                filteredSiparişler = filteredSiparişler.Where(s => s.sip_id == filterModel.SiparisId.Value);
            }

            if (filterModel.TeslimTarihStart.HasValue)
            {
                filteredSiparişler = filteredSiparişler.Where(s => s.teslim_tarih >= filterModel.TeslimTarihStart.Value);
            }

            if (filterModel.TeslimTarihEnd.HasValue)
            {
                filteredSiparişler = filteredSiparişler.Where(s => s.teslim_tarih <= filterModel.TeslimTarihEnd.Value);
            }

            return filteredSiparişler.ToList();
        }
    }
}
