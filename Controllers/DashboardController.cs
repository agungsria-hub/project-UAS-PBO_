using Microsoft.AspNetCore.Mvc;
using SistemRetribusiParkir.Services;

namespace SistemRetribusiParkir.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _service.GetDashboardAsync();

            return View(vm);
        }
    }
}