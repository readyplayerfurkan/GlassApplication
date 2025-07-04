using GlassApplication.Models.Abstract;

namespace GlassApplication.Models
{
    public class SiparişService : ISiparişService
    {
        private readonly ISiparişRepository _siparişRepository;

        public SiparişService(ISiparişRepository siparişRepository)
        {
            _siparişRepository = siparişRepository;
        }

        public async void ListByProcess(string process)
        {
            var siparişler = await _siparişRepository.GetSiparişlerAsync(process);

            process = "All"; // Değiştirmeyi unutma

            if (process == "All")
            {
                foreach (var sipariş in siparişler)
                    sipariş.IsItSearched = true;
            }
            else
            {
                foreach (var sipariş in siparişler)
                {
                    sipariş.IsItSearched = false;

                    if (sipariş.siparis_DRM == process)
                        sipariş.IsItSearched = true;
                }
            }
        }

        public void SiparişAra(string searchTerm, string filterType, string filterValue)
        {
            throw new NotImplementedException();
        }

        public async void UpdateSearchStatus(string searchTerm)
        {
            var siparişler = await _siparişRepository.GetSiparişlerAsync("Sevk/Teslim Edildi");

            if (string.IsNullOrWhiteSpace(searchTerm))
                return;

            foreach (var ürün in siparişler)
            {
                bool found = ürün.GetType()
                                    .GetProperties()
                                    .Where(p => p.PropertyType == typeof(string))
                                    .Select(p => p.GetValue(ürün) as string)
                                    .Any(value => value != null && value.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

                if (!found)
                {
                    found = ürün.GetType()
                                   .GetProperties()
                                   .Where(p => p.PropertyType == typeof(float?) || p.PropertyType == typeof(int?))
                                   .Select(p => p.GetValue(ürün)?.ToString())
                                   .Any(value => value != null && value.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                }

                ürün.IsItSearched = found;
            }
        }
    }
}
