using Microsoft.AspNetCore.Mvc;
using SistemRetribusiParkir.Models;
using SistemRetribusiParkir.Services;


namespace SistemRetribusiParkir.Controllers
{
    public class JenisKendaraanController : Controller
    {
        private readonly JenisKendaraanService _service;

        public JenisKendaraanController(JenisKendaraanService service)
        {
            _service = service;
        }

        // Menampilkan semua data
        public async Task<IActionResult> Index(string? search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                var hasil = await _service.SearchAsync(search);
                return View(hasil);
            }

            var data = await _service.GetAllAsync();
            return View(data);
        }

        // Menampilkan form tambah
        public IActionResult Create()
        {
            return View();
        }

        // Menyimpan data baru
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JenisKendaraan jenis)
        {
            if (!ModelState.IsValid)
                return View(jenis);

            await _service.AddAsync(jenis);

            TempData["Success"] = "Data berhasil ditambahkan.";

            return RedirectToAction(nameof(Index));
        }

        // Menampilkan form edit
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return View(data);
        }

        // Menyimpan hasil edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JenisKendaraan jenis)
        {
            if (id != jenis.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(jenis);

            await _service.UpdateAsync(jenis);

            TempData["Success"] = "Data berhasil diperbarui.";

            return RedirectToAction(nameof(Index));
        }

        // Menghapus data
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            TempData["Success"] = "Data berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetTarif(int id)
        {
            var jenis = await _service.GetByIdAsync(id);

            if (jenis == null)
            {
                return NotFound();
            }

            return Json(new
            {
                tarif = jenis.Tarif
            });
        }
    }
}