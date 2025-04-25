using GlassApplication.Models.Abstract;

namespace GlassApplication.Models
{
    public class ÜrünService : IÜrünService
    {
        private readonly IÜrünRepository _ürünRepository;

        public ÜrünService(IÜrünRepository ürünRepository)
        {
            _ürünRepository = ürünRepository;
        }

        public void ListByCategory(string category)
        {
            var ürünler = _ürünRepository.GetAll();

            if (category == "All")
            {
                foreach (var ürün in ürünler)
                    ürün.IsItSearched = true;
            }
            else
            {
                foreach (var ürün in ürünler)
                {
                    ürün.IsItSearched = false;

                    if (ürün.Kategori == category)
                        ürün.IsItSearched = true;
                }
            }
        }

        public void UpdateSearchStatus(string searchTerm)
        {
            var ürünler = _ürünRepository.GetAll();

            if (string.IsNullOrWhiteSpace(searchTerm))
                return;

            foreach (var ürün in ürünler)
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
