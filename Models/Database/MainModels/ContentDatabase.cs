using GlassApplication.Models.Database.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace GlassApplication.Models.Database;

public class ContentDatabase : DbContext
{
    public ContentDatabase(DbContextOptions options) : base(options) { }
    
    public DbSet<STOK> STOK { get; set; }
    public DbSet<STOK_TURU> STOK_TURU { get; set; }
    public DbSet<STOK_ANA_GRUP> STOK_ANA_GRUP { get; set; }
    public DbSet<STOK_ALT_GRUP> STOK_ALT_GRUP { get; set; }
    public DbSet<vw_SIPARIS_BASLIK_DETAY_PROCESSLI> vw_SIPARIS_BASLIK_DETAY_PROCESSLI { get; set; }
}