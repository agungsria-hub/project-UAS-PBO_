using Microsoft.AspNetCore.Mvc;
using SistemRetribusiParkir.Models.ViewModels;
using SistemRetribusiParkir.Services;

namespace SistemRetribusiParkir.Controllers
{
    public class RetribusiController : Controller
    {
        private readonly RetribusiService _service;

        public RetribusiController(RetribusiService service)
        {
            _service = service;
        }

        // ==========================
        // INDEX
        // ==========================

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        // ==========================
        // CREATE
        // ==========================

        public async Task<IActionResult> Create()
        {
            var vm = new RetribusiViewModel
            {
                LokasiList = await _service.GetLokasiAsync(),
                JenisKendaraanList = await _service.GetJenisKendaraanAsync(),
                PetugasList = await _service.GetPetugasAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RetribusiViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.LokasiList = await _service.GetLokasiAsync();
                vm.JenisKendaraanList = await _service.GetJenisKendaraanAsync();
                vm.PetugasList = await _service.GetPetugasAsync();

                return View(vm);
            }

            await _service.AddAsync(vm.Retribusi);

            TempData["Success"] = "Transaksi berhasil disimpan.";

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // EDIT
        // ==========================

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            var vm = new RetribusiViewModel
            {
                Retribusi = data,
                LokasiList = await _service.GetLokasiAsync(),
                JenisKendaraanList = await _service.GetJenisKendaraanAsync(),
                PetugasList = await _service.GetPetugasAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RetribusiViewModel vm)
        {
            if (id != vm.Retribusi.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                vm.LokasiList = await _service.GetLokasiAsync();
                vm.JenisKendaraanList = await _service.GetJenisKendaraanAsync();
                vm.PetugasList = await _service.GetPetugasAsync();

                return View(vm);
            }

            await _service.UpdateAsync(vm.Retribusi);

            TempData["Success"] = "Data transaksi berhasil diperbarui.";

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // DELETE
        // ==========================

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            TempData["Success"] = "Data transaksi berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // AJAX TARIF
        // ==========================

        [HttpGet]
        public async Task<IActionResult> GetTarif(int id)
        {
            var tarif = await _service.GetTarifAsync(id);

            return Json(tarif);
        }
    }
}