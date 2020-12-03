using RentAVehicle.Core.Models;
using RentAVehicle.Domain.Features.Vehicles;

namespace RentAVehicle.Domain.Features
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
