using GoalSetter.Core.Models;
using GoalSetter.Domain.Features.Rentals;

namespace GoalSetter.Domain.Features
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
