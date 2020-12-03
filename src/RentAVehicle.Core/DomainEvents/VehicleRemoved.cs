using System;

namespace RentAVehicle.Core.DomainEvents
{
    public class VehicleRemoved {
        public VehicleRemoved(DateTime deleted) => Deleted = deleted;
        public DateTime Deleted { get; set; }
    }
}
