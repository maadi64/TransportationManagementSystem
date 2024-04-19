using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Services;
using TMS.Application.ViewModels;

namespace TMS.UI.Controllers
{
    public class VehiclesController : BaseController<IVehiclesService>
    {
        public VehiclesController(IVehiclesService service) : base(service)
        {
        }
        // GET: VehiclesController
        public async Task<ActionResult> Index()
        {
            var vehicles = await _service.GetVehiclesAsync();
            return View(vehicles);
        }

        // GET: VehiclesController/Details/5
        public async Task<ActionResult> GetVehicleById(int id)
        {
            var vehicle = await _service.GetVehicleById(id);
            return View(vehicle);
        }

        // GET: VehiclesController/Create
        public async Task<ActionResult> Create(int? id = null)
        {
            if (id != null && id > 0)
            {
                var vehicle = await _service.GetVehicleById(id.Value);
                return View(vehicle);
            }
            return View(new VehicleViewModel());
        }

        // POST: VehiclesController/Create
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(VehicleViewModel vehicleViewModel)
        {
            try
            {
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
                    await _service.PutVehiclesAsync(vehicleViewModel);
                else
                    await _service.PostVehiclesAsync(vehicleViewModel);
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
                await _service.DeleteVehiclesAsync(id);
            }
            catch
            {
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
