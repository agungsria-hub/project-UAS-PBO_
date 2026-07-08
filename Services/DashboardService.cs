using Microsoft.EntityFrameworkCore;
using SistemRetribusiParkir.Data;
using SistemRetribusiParkir.Models.ViewModels;

namespace SistemRetribusiParkir.Services
{
    public class DashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardAsync()
        {
            var vm = new DashboardViewModel();

            vm.TotalLokasi = await _context.LokasiParkir.CountAsync();

            vm.TotalJenisKendaraan = await _context.JenisKendaraan.CountAsync();

            vm.TotalPetugas = await _context.Petugas.CountAsync();

            vm.TotalTransaksi = await _context.Retribusi.CountAsync();

            vm.TotalPendapatan = await _context.Retribusi
                .SumAsync(x => (decimal?)x.JumlahBayar) ?? 0;

            var grafik = await _context.Retribusi
                .GroupBy(x => x.Tanggal.Date)
                .Select(x => new
                {
                    Tanggal = x.Key,
                    Total = x.Sum(y => y.JumlahBayar)
                })
                .OrderBy(x => x.Tanggal)
                .Take(7)
                .ToListAsync();

            vm.LabelGrafik = grafik
                .Select(x => x.Tanggal.ToString("dd/MM"))
                .ToList();

            vm.DataGrafik = grafik
                .Select(x => x.Total)
                .ToList();

            return vm;
        }
    }
}