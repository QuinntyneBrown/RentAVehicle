using System;

namespace RentAVehicle.Core.DomainEvents
{
    public class ClientRemoved
    {
        public ClientRemoved(DateTime deleted)
        {
            Deleted = deleted;
        }

        public DateTime Deleted { get; }
    }
}
