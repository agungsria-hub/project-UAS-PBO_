using System.ComponentModel.DataAnnotations;

namespace SistemRetribusiParkir.Models
{
    public class Petugas
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama petugas wajib diisi.")]
        [Display(Name = "Nama Petugas")]
        [StringLength(100)]
        public string NamaPetugas { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username wajib diisi.")]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nomor HP wajib diisi.")]
        [Display(Name = "Nomor HP")]
        [StringLength(20)]
        public string NoHp { get; set; } = string.Empty;

        [Required(ErrorMessage = "Alamat wajib diisi.")]
        [StringLength(200)]
        public string Alamat { get; set; } = string.Empty;

        [Display(Name = "Status Aktif")]
        public bool Status { get; set; } = true;

        // Navigation Property
        public ICollection<Retribusi>? Retribusi { get; set; }
    }
}