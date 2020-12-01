using BuildingBlocks.Abstractions;
using GoalSetter.Core.DomainEvents;
using System;

namespace GoalSetter.Core.Models
{
    public class Vehicle: AggregateRoot
    {
        public Vehicle(string make, string model, decimal dailyRate)
            => Apply(new VehicleAdded(Guid.NewGuid(), make, model, dailyRate));
        protected override void When(dynamic @event) => When(@event);

        public void When(VehicleAdded vehicleAdded)
        {
            VehicleId = vehicleAdded.VehicleId;
            Make = vehicleAdded.Make;
            Model = vehicleAdded.Model;
            DailyRate = vehicleAdded.DailyRate;
        }

        public void When(VehicleRemoved vehicleRemoved)
        {
            Deleted = vehicleRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model))
                throw new Exception("Model Invalid Exception");
        }

        public void Remove()
        {
            Apply(new VehicleRemoved(DateTime.UtcNow));
        }

        public Guid VehicleId { get; private set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal DailyRate { get; set; }
        public DateTime Deleted { get; set; }
    }
}
