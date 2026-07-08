namespace SistemRetribusiParkir.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalLokasi { get; set; }

        public int TotalJenisKendaraan { get; set; }

        public int TotalPetugas { get; set; }

        public int TotalTransaksi { get; set; }

        public decimal TotalPendapatan { get; set; }

        public List<string> LabelGrafik { get; set; } = new();

        public List<decimal> DataGrafik { get; set; } = new();
    }
}