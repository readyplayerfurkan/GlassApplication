using GlassApplication.Models.Abstract;
using GlassApplication.Models.Database;
using GlassApplication.Models.Database.DatabaseModels;

namespace GlassApplication.Models
{
    public class ÜrünRepository : IÜrünRepository
    {
        private readonly List<ÜrünModel> Ürünler = [];
        private readonly GW_TEST_2025 _dbContext;

        public ÜrünRepository(GW_TEST_2025 dbcontext)
        {
            _dbContext = dbcontext;

            Ürünler = (from s in _dbContext.STOK
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
        }
        public List<ÜrünModel> GetAll()
        {
            return Ürünler;
        }
    }
}
