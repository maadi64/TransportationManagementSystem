using AutoMapper;
using MathNet.Numerics.Distributions;
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
    public class VehiclesService : IVehiclesService
    {
        public IBaseRepository<Vehicle> _baseVehiclesRepository { get; set; }
       
        private readonly IMapper _mapper;

        public VehiclesService(IBaseRepository<Vehicle> baseVehiclesRepository,IMapper mapper)
        {
            _baseVehiclesRepository = baseVehiclesRepository;
            _mapper = mapper;
        }

        public async Task<VehicleViewModel> GetVehicleById(int id)
        {
            var vehicle = await _baseVehiclesRepository.GetByIdAsync("Id", id);
            return _mapper.Map<VehicleViewModel>(vehicle);
        }


        public async Task<List<VehicleViewModel>> GetVehiclesAsync()
        {
            var vehicles = await _baseVehiclesRepository.GetAsync();
            return _mapper.Map<List<VehicleViewModel>>(vehicles);
        }

        public async Task<int> PostVehiclesAsync(VehicleViewModel vehicleModel)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleModel);
            return await _baseVehiclesRepository.PostAsync(vehicle);
        }

        public async Task<bool> PutVehiclesAsync(VehicleViewModel vehicleModel)
        {
            var inventories = _mapper.Map<Vehicle>(vehicleModel);
            return await _baseVehiclesRepository.UpdateAsync("Id", inventories);
        }

        public async Task<bool> DeleteVehiclesAsync(int id)
        {
            return await _baseVehiclesRepository.DeleteAsync("Id", id);
        }
    }
}
