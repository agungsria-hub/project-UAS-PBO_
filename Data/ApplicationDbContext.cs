using Microsoft.EntityFrameworkCore;
using SistemRetribusiParkir.Models;

namespace SistemRetribusiParkir.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LokasiParkir> LokasiParkir { get; set; }

        public DbSet<JenisKendaraan> JenisKendaraan { get; set; }
        public DbSet<Petugas> Petugas { get; set; }
        public DbSet<Retribusi> Retribusi { get; set; }
    }
}