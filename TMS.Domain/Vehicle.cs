using TMS.Common.Enums;

namespace TMS.Domain
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public string Rego { get; set; }
        public double Rent { get; set; }
        public double Bond { get; set; }
        public FuelType FuelType { get; set; }
        public double Mileage { get; set; }
        public bool IsAvailable { get; set; }
        public string EngineCapacity { get; set; }
    }
}
