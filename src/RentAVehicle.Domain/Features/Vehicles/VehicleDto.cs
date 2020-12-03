using System;

namespace RentAVehicle.Domain.Features.Vehicles
{
    public class VehicleDto
    {
        public Guid VehicleId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal DailyRate { get; set; }
    }
}
