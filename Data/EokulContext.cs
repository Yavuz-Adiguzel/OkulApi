using Microsoft.EntityFrameworkCore;
using OkulApi.Models;

namespace OkulApi.Data
{
    public class EokulContext : DbContext
    {
        public EokulContext(DbContextOptions<EokulContext> options) : base(options) { }

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<OgrenciDers> OgrenciDersler { get; set; }
    }
}
