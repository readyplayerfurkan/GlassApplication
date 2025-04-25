using Microsoft.EntityFrameworkCore;

namespace GlassApplication.Models.Database
{
    public class GW_SISTEM : DbContext
    {
        public GW_SISTEM(DbContextOptions<GW_SISTEM> options) : base(options) { }

        public DbSet<Kullanici> KULLANICI { get; set; }
        public DbSet<KullaniciYetki> KULLANICI_YETKI { get; set; }
        public DbSet<Firma> FIRMA { get; set; }
    }
}
