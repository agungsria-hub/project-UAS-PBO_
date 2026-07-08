using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemRetribusiParkir.Models
{
    public class Retribusi
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tanggal wajib diisi.")]
        [Display(Name = "Tanggal")]
        public DateTime Tanggal { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Nomor kendaraan wajib diisi.")]
        [StringLength(20)]
        [Display(Name = "Nomor Polisi")]
        public string NomorKendaraan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lokasi parkir wajib dipilih.")]
        [Display(Name = "Lokasi Parkir")]
        public int LokasiParkirId { get; set; }

        [ForeignKey(nameof(LokasiParkirId))]
        public LokasiParkir? LokasiParkir { get; set; }

        [Required(ErrorMessage = "Jenis kendaraan wajib dipilih.")]
        [Display(Name = "Jenis Kendaraan")]
        public int JenisKendaraanId { get; set; }

        [ForeignKey(nameof(JenisKendaraanId))]
        public JenisKendaraan? JenisKendaraan { get; set; }

        [Required(ErrorMessage = "Petugas wajib dipilih.")]
        [Display(Name = "Petugas")]
        public int PetugasId { get; set; }

        [ForeignKey(nameof(PetugasId))]
        public Petugas? Petugas { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Tarif")]
        public decimal Tarif { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Jumlah Bayar")]
        public decimal JumlahBayar { get; set; }

        [StringLength(200)]
        [Display(Name = "Keterangan")]
        public string? Keterangan { get; set; }
    }
}