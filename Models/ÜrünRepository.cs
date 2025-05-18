using GlassApplication.Models.Abstract;
using GlassApplication.Models.Database;

namespace GlassApplication.Models
{
    public class ÜrünRepository : IÜrünRepository
    {
        private readonly GW_TEST_2025 _dbContext;

        public ÜrünRepository(GW_TEST_2025 dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ÜrünModel> GetAll(string category, string searchTerm, bool IsItSearched)
        {
            // Veritabanından tüm ürünleri çekiyoruz
            var ürünler = (from s in _dbContext.STOK
                           join t in _dbContext.STOK_TURU on (int)s.cam_tek_cift equals t.ID into tGroup
                           from t in tGroup.DefaultIfEmpty()
                           join a in _dbContext.STOK_ANA_GRUP on s.ana_grup equals a.id into aGroup
                           from a in aGroup.DefaultIfEmpty()
                           join l in _dbContext.STOK_ALT_GRUP on s.alt_grup equals l.id into lGroup
                           from l in lGroup.DefaultIfEmpty()
                           select new ÜrünModel
                           {
                               Kategori = t != null ? t.ADI : null,
                               Ad = s != null ? s.adi : null,
                               Açıklama = a != null ? a.adi : null,
                               Renk = s.renk,
                               Kalınlık = (float?)s.cam_kal1,
                               IsItSearched = true,
                           }).ToList(); // ToList ile belleğe alıyoruz

            if (IsItSearched && !string.IsNullOrEmpty(searchTerm))
            {
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

                ürünler = ürünler.Where(x => x.IsItSearched).ToList();
            }
            else if (!string.IsNullOrEmpty(category))
            {
                ürünler = ürünler.Where(x => x.Kategori?.Contains(category) == true).ToList();
            }

            return ürünler;
        }
    }
}
