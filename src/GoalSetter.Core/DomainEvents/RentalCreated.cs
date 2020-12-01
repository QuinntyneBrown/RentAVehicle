using GoalSetter.Core.Models;
using System;

namespace GoalSetter.Core.DomainEvents
{
    public class RentalCreated
    {
        public RentalCreated(Guid rentalId, Guid vehicleId, Guid clientId, DateRange dateRange, decimal total)
            => (RentalId, VehicleId, ClientId, DateRange, Total) = (rentalId, vehicleId, clientId, dateRange, total);

        public Guid RentalId { get; }
        public Guid VehicleId { get; set; }
        public Guid ClientId { get; set; }
        public DateRange DateRange { get; set; }
        public decimal Total { get; set; }
    }
}
