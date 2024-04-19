using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.ViewModels;

namespace TMS.Application.Services
{
    public interface IVehiclesService
    {
        Task<bool> DeleteVehiclesAsync(int id);
        Task<VehicleViewModel> GetVehicleById(int id);
        Task<List<VehicleViewModel>> GetVehiclesAsync();
        Task<int> PostVehiclesAsync(VehicleViewModel vehicleModel);
        Task<bool> PutVehiclesAsync(VehicleViewModel vehicleModel);
    }
}
