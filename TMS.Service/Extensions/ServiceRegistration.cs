using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Services;

namespace TMS.Service.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IVehiclesService, VehiclesService>();
            services.AddScoped<IVehicleMaintenanceHistoriesService, VehicleMaintenanceHistoriesService>();
        }
    }
}
