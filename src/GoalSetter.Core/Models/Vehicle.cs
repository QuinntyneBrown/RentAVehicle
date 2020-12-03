using BuildingBlocks.Abstractions;
using GoalSetter.Core.DomainEvents;
using System;

namespace GoalSetter.Core.Models
{
    public class Vehicle: AggregateRoot
    {
        public Vehicle(int year, string make, string model, Guid dailyRateId)
            => Apply(new VehicleAdded(Guid.NewGuid(), year, make, model, dailyRateId));
        protected override void When(dynamic @event) => When(@event);

        public void When(VehicleAdded vehicleAdded)
        {
            VehicleId = vehicleAdded.VehicleId;
            Make = vehicleAdded.Make;
            Model = vehicleAdded.Model;
            Year = vehicleAdded.Year;
            DailyRateId = vehicleAdded.DailyRateId;
        }

        public void When(VehicleRemoved vehicleRemoved)
        {
            Deleted = vehicleRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || Year == default)
            {
                throw new Exception("Model Invalid");         
            }
        }

        public void Remove(DateTime dateTime)
        {
            Apply(new VehicleRemoved(dateTime));
        }

        public Guid VehicleId { get; private set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public Guid DailyRateId { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
