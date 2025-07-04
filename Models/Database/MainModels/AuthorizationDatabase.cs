using Microsoft.EntityFrameworkCore;

namespace GlassApplication.Models.Database
{
    public class AuthorizationDatabase : DbContext
    {
        public AuthorizationDatabase(DbContextOptions<AuthorizationDatabase> options) : base(options) { }

        public DbSet<KULLANICI> KULLANICI { get; set; }
        public DbSet<KULLANICI_YETKI> KULLANICI_YETKI { get; set; }
        public DbSet<FIRMA> FIRMA { get; set; }
    }
}
