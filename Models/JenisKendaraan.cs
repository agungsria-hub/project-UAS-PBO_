using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemRetribusiParkir.Models
{
    public class JenisKendaraan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama jenis kendaraan wajib diisi.")]
        [Display(Name = "Jenis Kendaraan")]
        [StringLength(50)]
        public string NamaJenis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tarif wajib diisi.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Tarif Parkir")]
        [Range(1000, 100000)]
        public decimal Tarif { get; set; }

        [Display(Name = "Keterangan")]
        [StringLength(100)]
        public string? Keterangan { get; set; }

        [Display(Name = "Status Aktif")]
        public bool Status { get; set; } = true;

        // Navigation Property
        public ICollection<Retribusi>? Retribusi { get; set; }
        
    }
}