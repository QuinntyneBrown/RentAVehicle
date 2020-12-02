using System;

namespace GoalSetter.Core.DomainEvents
{
    public class VehicleAdded
    {
        public VehicleAdded(Guid vehicleId, int year, string make, string model, Guid dailyRateId)
             => (VehicleId, Year, Make, Model, DailyRateId) = (vehicleId, year, make, model, dailyRateId);

        public Guid VehicleId { get; set; }
        public int Year { get; set; }
        public string Make { get; }
        public string Model { get; set; }
        public Guid DailyRateId { get; set; }
    }
}
