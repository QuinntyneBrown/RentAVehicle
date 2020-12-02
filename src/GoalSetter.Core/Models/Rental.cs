using BuildingBlocks.Abstractions;
using GoalSetter.Core.DomainEvents;
using GoalSetter.Core.ValueObjects;
using System;

namespace GoalSetter.Core.Models
{
    public class Rental: AggregateRoot
    {
        public Rental(Guid vehicleId, Guid clientId, DateRange dateRange, Price total)
        {
            Apply(new RentalCreated(
                Guid.NewGuid(),
                vehicleId,
                clientId,
                dateRange,
                total));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(RentalCreated rentalCreated)
        {
            RentalId = rentalCreated.RentalId;
            ClientId = rentalCreated.ClientId;
            VehicleId = rentalCreated.VehicleId;
            DateRange = rentalCreated.DateRange;
            Total = rentalCreated.Total;
        }

        public void When(RentalCancelled rentalCancelled)
        {
            Cancelled = rentalCancelled.Cancelled;
        }

        protected override void EnsureValidState()
        {

        }

        public void Cancel(DateTime dateTime)
        {
            Apply(new RentalCancelled(dateTime));
        }

        public Guid RentalId { get; private set; }
        public Guid VehicleId { get; set; }
        public Guid ClientId { get; set; }
        public Price Total { get; set; }
        public DateRange DateRange { get; set; }
        public DateTime? Cancelled { get; set; }
    }
}
