using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.ViewModels;

namespace TMS.Application.Services
{
    public interface IVehicleMaintenanceHistoriesService
    {
        Task<bool> DeleteVehicleMaintenanceHistoriesAsync(int id);
        Task<List<VehicleMaintenanceHistoryViewModel>> GetVehicleMaintenanceHistoriesAsync();
        Task<VehicleMaintenanceHistoryViewModel> GetVehicleMaintenanceHistoryById(int id);
        Task<int> PostVehicleMaintenanceHistoriesAsync(VehicleMaintenanceHistoryViewModel vehicleMaintenanceModel);
        Task<bool> PutVehicleMaintenanceHistoriesAsync(VehicleMaintenanceHistoryViewModel vehicleMaintenanceModel);
    }
}
