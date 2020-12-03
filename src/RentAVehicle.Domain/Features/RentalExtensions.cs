using RentAVehicle.Core.Models;
using RentAVehicle.Domain.Features.Rentals;

namespace RentAVehicle.Domain.Features
{
    public static class RentalExtensions
    {
        public static RentalDto ToDto(this Rental rental)
        {
            return new RentalDto
            {
                RentalId = rental.RentalId,
                VehicleId = rental.VehicleId,
                ClientId = rental.ClientId,
                Total = rental.Total,
                Start = rental.DateRange.StartDate,
                End = rental.DateRange.EndDate
            };
        }
    }
}
