using Microsoft.AspNetCore.Mvc;
using SistemRetribusiParkir.Services;

namespace SistemRetribusiParkir.Controllers
{
    public class LaporanController : Controller
    {
        private readonly LaporanService _service;

        public LaporanController(LaporanService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(
            DateTime? tanggalAwal,
            DateTime? tanggalAkhir)
        {
            var vm = await _service.GetLaporanAsync(
                tanggalAwal,
                tanggalAkhir);

            return View(vm);
        }
        public async Task<IActionResult> ExportPdf(
    DateTime? tanggalAwal,
    DateTime? tanggalAkhir)
        {
            var vm = await _service.GetLaporanAsync(
                tanggalAwal,
                tanggalAkhir);

            var pdf = _service.ExportPdf(vm.Data);

            return File(
                pdf,
                "application/pdf",
                "LaporanRetribusi.pdf");
        }
        public async Task<IActionResult> ExportExcel(
    DateTime? tanggalAwal,
    DateTime? tanggalAkhir)
        {
            var vm = await _service.GetLaporanAsync(
                tanggalAwal,
                tanggalAkhir);

            var excel = _service.ExportExcel(vm.Data);

            return File(
                excel,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "LaporanRetribusi.xlsx");
        }
    }
}