using Microsoft.AspNetCore.Mvc.Rendering;
using SistemRetribusiParkir.Models;

namespace SistemRetribusiParkir.Models.ViewModels
{
    public class RetribusiViewModel
    {
        public Retribusi Retribusi { get; set; } = new();

        public IEnumerable<SelectListItem> LokasiList { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> JenisKendaraanList { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> PetugasList { get; set; }
            = new List<SelectListItem>();
    }
}