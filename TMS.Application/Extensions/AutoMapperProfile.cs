using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.ViewModels;
using TMS.Domain;

namespace TMS.Application.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Vehicle, VehicleViewModel>().ReverseMap();
            CreateMap<VehicleMaintenanceHistory, VehicleMaintenanceHistoryViewModel>().ReverseMap();
        }
    }
}
