using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.Generic;
using TMS.Application.Repository;
using TMS.Application.Services;
using TMS.Application.ViewModels;
using TMS.Domain;

namespace TMS.Service
{
    public class VehicleMaintenanceHistoriesService : IVehicleMaintenanceHistoriesService
    {
        public IBaseRepository<VehicleMaintenanceHistory> _baseVehicleMaintenanceHistoryRepository { get; set; }
        private readonly IMapper _mapper;

        public VehicleMaintenanceHistoriesService(IBaseRepository<VehicleMaintenanceHistory> baseVehicleMaintenanceHistoryRepository,  IMapper mapper)
        {
            _baseVehicleMaintenanceHistoryRepository = baseVehicleMaintenanceHistoryRepository;
            _mapper = mapper;
        }

        public async Task<VehicleMaintenanceHistoryViewModel> GetVehicleMaintenanceHistoryById(int id)
        {
            var vehicle = await _baseVehicleMaintenanceHistoryRepository.GetByIdAsync("Id", id);
            return _mapper.Map<VehicleMaintenanceHistoryViewModel>(vehicle);
        }

        public async Task<List<VehicleMaintenanceHistoryViewModel>> GetVehicleMaintenanceHistoriesAsync()
        {
            var vehicleMaintenanceHistories = await _baseVehicleMaintenanceHistoryRepository.GetAsync();
            return _mapper.Map<List<VehicleMaintenanceHistoryViewModel>>(vehicleMaintenanceHistories);
        }

        public async Task<int> PostVehicleMaintenanceHistoriesAsync(VehicleMaintenanceHistoryViewModel vehicleMaintenanceModel)
        {
            var vehicle = _mapper.Map<VehicleMaintenanceHistory>(vehicleMaintenanceModel);
            return await _baseVehicleMaintenanceHistoryRepository.PostAsync(vehicle);
        }

        public async Task<bool> PutVehicleMaintenanceHistoriesAsync(VehicleMaintenanceHistoryViewModel VehicleMaintenanceHistoryViewModel)
        {
            var VehicleMaintenanceHistory = _mapper.Map<VehicleMaintenanceHistory>(VehicleMaintenanceHistoryViewModel);
            return await _baseVehicleMaintenanceHistoryRepository.UpdateAsync("Id", VehicleMaintenanceHistory);
        }

        public async Task<bool> DeleteVehicleMaintenanceHistoriesAsync(int id)
        {
            return await _baseVehicleMaintenanceHistoryRepository.DeleteAsync("Id", id);
        }
    }
}
