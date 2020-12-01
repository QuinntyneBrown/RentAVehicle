using GoalSetter.Core.Models;
using GoalSetter.Domain.Features.Vehicles;

namespace GoalSetter.Domain.Features
{
    public static class VehicleExtensions
    {
        public static VehicleDto ToDto(this Vehicle vehicle, DailyRate dailyRate)
        {
            return new VehicleDto
            {
                VehicleId = vehicle.VehicleId,
                Make = vehicle.Make,
                Model = vehicle.Model,
                DailyRate = dailyRate.Price
            };
        }
    }
}
