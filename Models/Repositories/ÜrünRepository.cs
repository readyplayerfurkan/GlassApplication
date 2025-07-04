using System.Text.Json;
using GlassApplication.Models.Abstract;
using GlassApplication.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace GlassApplication.Models
{
    public class ÜrünRepository : IÜrünRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ÜrünRepository(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public List<ÜrünModel> GetAll(string category, string searchTerm, bool IsItSearched)
        {
            var firmalarJson = _httpContextAccessor.HttpContext.Session.GetString("Firmalar");
            var firmalar = JsonSerializer.Deserialize<List<Firma>>(firmalarJson);
            var firmaId = _httpContextAccessor.HttpContext.Session.GetInt32("SeçilenFirmaID");
            
            var seciliFirma = firmalar.Find(x => x.ID == firmaId);
           
            var cariId = _httpContextAccessor.HttpContext.Session.GetInt32("SeciliCariId");
            var baseConnection = _configuration.GetConnectionString("ContentDatabaseConnection");
            var connectionString = $"{baseConnection};Database={seciliFirma.KOD}";

            var options = new DbContextOptionsBuilder<ContentDatabase>()
                .UseSqlServer(connectionString)
                .Options;
            
            using var _dbContext = new ContentDatabase(options);

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
                           }).ToList();

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
