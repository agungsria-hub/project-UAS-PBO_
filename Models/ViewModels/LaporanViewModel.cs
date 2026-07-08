using SistemRetribusiParkir.Models;

namespace SistemRetribusiParkir.Models.ViewModels
{
    public class LaporanViewModel
    {
        public DateTime? TanggalAwal { get; set; }

        public DateTime? TanggalAkhir { get; set; }

        public List<Retribusi> Data { get; set; } = new();

        public decimal TotalPendapatan { get; set; }
    }
}