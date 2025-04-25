using GlassApplication.Models.Database.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace GlassApplication.Models.Database
{
    public class GW_TEST_2025 : DbContext
    {
        public GW_TEST_2025(DbContextOptions options) : base(options) { }
        
        public DbSet<STOK> STOK { get; set; }
        public DbSet<STOK_TURU> STOK_TURU { get; set; }
        public DbSet<STOK_ANA_GRUP> STOK_ANA_GRUP { get; set; }
        public DbSet<STOK_ALT_GRUP> STOK_ALT_GRUP { get; set; }
        public DbSet<SiparişModel> vw_SIPARIS_BASLIK_DETAY { get; set; }
        public DbSet<SiparişDetayModel> vw_SIPARIS_DETAY { get; set; }
    }
}
