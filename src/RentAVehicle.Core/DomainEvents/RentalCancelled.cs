using System;

namespace RentAVehicle.Core.DomainEvents
{
    public class RentalCancelled
    {
        public RentalCancelled(DateTime dateTime)
        {
            Cancelled = dateTime;
        }

        public DateTime Cancelled { get; }
    }
}
