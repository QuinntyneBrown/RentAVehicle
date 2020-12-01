using System;

namespace GoalSetter.Domain.Features.Vehicles
{
    public class VehicleDto
    {
        public Guid VehicleId { get; private set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal DailyRate { get; set; }
        public DateTime Deleted { get; set; }

    }
}
