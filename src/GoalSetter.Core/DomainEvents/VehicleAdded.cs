using System;

namespace GoalSetter.Core.DomainEvents
{
    public class VehicleAdded
    {
        public VehicleAdded(Guid vehicleId, string make, string model, Guid dailyRateId)
             => (VehicleId, Make, Model, DailyRateId) = (vehicleId, make, model, dailyRateId);

        public Guid VehicleId { get; set; }
        public string Make { get; }
        public string Model { get; set; }
        public Guid DailyRateId { get; set; }
    }
}
