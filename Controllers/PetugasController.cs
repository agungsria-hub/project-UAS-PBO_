using Microsoft.AspNetCore.Mvc;
using SistemRetribusiParkir.Models;
using SistemRetribusiParkir.Services;

namespace SistemRetribusiParkir.Controllers
{
    public class PetugasController : Controller
    {
        private readonly PetugasService _service;

        public PetugasController(PetugasService service)
        {
            _service = service;
        }

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Petugas petugas)
        {
            if (!ModelState.IsValid)
                return View(petugas);

            await _service.AddAsync(petugas);

            TempData["Success"] = "Data petugas berhasil ditambahkan.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Petugas petugas)
        {
            if (id != petugas.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(petugas);

            await _service.UpdateAsync(petugas);

            TempData["Success"] = "Data petugas berhasil diperbarui.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            TempData["Success"] = "Data petugas berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }
    }
}