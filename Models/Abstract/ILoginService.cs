namespace GlassApplication.Models.Abstract
{
    public interface ILoginService
    {
        Task<bool> Login(string username, string password);

    }
}
