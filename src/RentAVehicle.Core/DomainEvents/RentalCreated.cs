using RentAVehicle.Core.Models;
using RentAVehicle.Core.ValueObjects;
using System;

namespace RentAVehicle.Core.DomainEvents
{
    public class RentalCreated
    {
        public RentalCreated(Guid rentalId, Guid vehicleId, Guid clientId, DateRange dateRange, Price total)
            => (RentalId, VehicleId, ClientId, DateRange, Total) = (rentalId, vehicleId, clientId, dateRange, total);

        public Guid RentalId { get; }
        public Guid VehicleId { get; set; }
        public Guid ClientId { get; set; }
        public DateRange DateRange { get; set; }
        public Price Total { get; set; }
    }
}
