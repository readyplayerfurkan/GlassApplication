namespace GlassApplication.Models.Abstract
{
    public interface ISiparişRepository
    {
        public Task<List<SiparişModel>> GetSiparişlerAsync(string siparişDurumu);
    }
}
