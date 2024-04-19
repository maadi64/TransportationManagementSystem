using System.ComponentModel.DataAnnotations;
using TMS.Common.Enums;

namespace TMS.Domain
{
    public class VehicleMaintenanceHistory
    {
        public int Id { get; set; }
        [Required]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        [Required]
        public double Expenses { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public MaintenanceType MaintenanceType { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}
