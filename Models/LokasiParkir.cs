using System.ComponentModel.DataAnnotations;

namespace SistemRetribusiParkir.Models
{
    public class LokasiParkir
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama lokasi wajib diisi.")]
        [StringLength(100)]
        [Display(Name = "Nama Lokasi")]
        public string NamaLokasi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Alamat wajib diisi.")]
        [StringLength(200)]
        public string Alamat { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kapasitas wajib diisi.")]
        [Range(1, 10000)]
        public int Kapasitas { get; set; }

        [Display(Name = "Status Aktif")]
        public bool Status { get; set; } = true;
    }
}