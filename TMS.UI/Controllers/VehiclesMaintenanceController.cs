using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Services;
using TMS.Application.ViewModels;

namespace TMS.UI.Controllers
{
    public class VehiclesMaintenanceController : BaseController<IVehicleMaintenanceHistoriesService>
    {
        private readonly IVehiclesService _vehiclesService;
        public VehiclesMaintenanceController(IVehicleMaintenanceHistoriesService service, IVehiclesService vehiclesService) : base(service)
        {
            _vehiclesService = vehiclesService;
        }
        // GET: VehicleMaintenanceHistoryController
        public async Task<ActionResult> Index()
        {
            var VehicleMaintenanceHistory = await _service.GetVehicleMaintenanceHistoriesAsync();
            return View(VehicleMaintenanceHistory);
        }

        // GET: VehicleMaintenanceHistoryController/Details/5
        public async Task<ActionResult> GetVehicleMaintenaceById(int id)
        {
            var vehicle = await _service.GetVehicleMaintenanceHistoryById(id);
            return View(vehicle);
        }

        // GET: VehicleMaintenanceHistoryController/Create
        public async Task<ActionResult> Create(int? id = null)
        {
            ViewBag.Vehicles = await _vehiclesService.GetVehiclesAsync();

            if (id != null && id > 0)
            {
                var vehicle = await _service.GetVehicleMaintenanceHistoryById(id.Value);
                return View(vehicle);
            }
            return View(new VehicleMaintenanceHistoryViewModel());
        }

        // POST: VehicleMaintenanceHistoryController/Create
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(VehicleMaintenanceHistoryViewModel vehicleViewModel)
        {
            try
            {
                ViewBag.Vehicles = await _vehiclesService.GetVehiclesAsync();
                if (!ModelState.IsValid)
                {
                    List<string> errors = new List<string>();
                    foreach (var value in ModelState.Values)
                    {
                        foreach (var error in value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }

                    ViewBag.ValidationErrors = errors;
                    return View(vehicleViewModel);
                }
                if (vehicleViewModel.Id > 0)
                    await _service.PutVehicleMaintenanceHistoriesAsync(vehicleViewModel);
                else
                    await _service.PostVehicleMaintenanceHistoriesAsync(vehicleViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(vehicleViewModel);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteVehicleMaintenanceHistoriesAsync(id);
            }
            catch
            {
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
