using System;

namespace GoalSetter.Core.DomainEvents
{
    public class VehicleAdded
    {
        public VehicleAdded(Guid vehicleId, string make, string model, decimal dailyRate)
             => (VehicleId, Make, Model, DailyRate) = (vehicleId, make, model, dailyRate);

        public Guid VehicleId { get; set; }
        public string Make { get; }
        public string Model { get; set; }
        public decimal DailyRate { get; set; }
    }
}
