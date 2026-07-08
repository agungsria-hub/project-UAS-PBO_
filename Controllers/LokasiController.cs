using Microsoft.AspNetCore.Mvc;
using SistemRetribusiParkir.Models;
using SistemRetribusiParkir.Services;

namespace SistemRetribusiParkir.Controllers
{
    public class LokasiController : Controller
    {
        private readonly LokasiService _service;

        public LokasiController(LokasiService service)
        {
            _service = service;
        }

        // Menampilkan seluruh data atau hasil pencarian
        public async Task<IActionResult> Index(string? search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                var hasil = await _service.SearchAsync(search);
                return View(hasil);
            }

            var lokasi = await _service.GetAllAsync();
            return View(lokasi);
        }

        // Menampilkan form tambah data
        public IActionResult Create()
        {
            return View();
        }

        // Menyimpan data baru
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LokasiParkir lokasi)
        {
            if (!ModelState.IsValid)
            {
                return View(lokasi);
            }

            await _service.AddAsync(lokasi);

            TempData["Success"] = "Data lokasi parkir berhasil ditambahkan.";

            return RedirectToAction(nameof(Index));
        }

        // Menampilkan form edit
        public async Task<IActionResult> Edit(int id)
        {
            var lokasi = await _service.GetByIdAsync(id);

            if (lokasi == null)
            {
                return NotFound();
            }

            return View(lokasi);
        }

        // Menyimpan perubahan data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LokasiParkir lokasi)
        {
            if (id != lokasi.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState)
                {
                    foreach (var error in item.Value.Errors)
                    {
                        Console.WriteLine($"{item.Key} : {error.ErrorMessage}");
                    }
                }

                return View(lokasi);
            }

            try
            {
                await _service.UpdateAsync(lokasi);

                TempData["Success"] = "Data berhasil diubah.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                ModelState.AddModelError("", ex.InnerException?.Message ?? ex.Message);

                return View(lokasi);
            }
        }

        // Menghapus data
        public async Task<IActionResult> Delete(int id)
        {
            var lokasi = await _service.GetByIdAsync(id);

            if (lokasi == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);

            TempData["Success"] = "Data lokasi parkir berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }
    }
}