using System.ComponentModel.DataAnnotations;
using TMS.Common.Enums;
using TMS.Domain;

namespace TMS.Application.ViewModels
{
    public class VehicleMaintenanceHistoryViewModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public double Expenses { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage ="Please fill Note Field")]
        public string Notes { get; set; }
        public MaintenanceType MaintenanceType { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}
