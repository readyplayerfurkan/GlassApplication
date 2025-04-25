namespace GlassApplication.Models.Abstract
{
    public interface IÜrünService
    {
        void UpdateSearchStatus(string searchTerm);
        void ListByCategory(string category);
    }
}
