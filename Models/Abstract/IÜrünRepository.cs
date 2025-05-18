namespace GlassApplication.Models.Abstract
{
    public interface IÜrünRepository
    {
        List<ÜrünModel> GetAll(string category, string searchTerm, bool IsItSearched);
    }
}
