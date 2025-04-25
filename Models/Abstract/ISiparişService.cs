namespace GlassApplication.Models.Abstract
{
    public interface ISiparişService
    {
        public void SiparişAra(string searchTerm, string filterType, string filterValue);
        public void ListByProcess(string process);       
    }
}
