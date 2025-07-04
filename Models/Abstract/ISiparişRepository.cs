using GlassApplication.Models.Database.TableModels.ContentTables;

namespace GlassApplication.Models.Abstract
{
    public interface ISiparişRepository
    {
        public Task<List<SiparisModel>> GetSiparişlerAsync(string siparişDurumu);
    }
}
