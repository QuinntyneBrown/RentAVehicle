using GoalSetter.Domain.Features.Vehicles;

namespace GoalSetter.Testing.Builders
{
    public class VehicleDtoBuilder
    {
        private VehicleDto _vehicleDto;

        public VehicleDtoBuilder()
        {
            _vehicleDto = new VehicleDto()
            {
                DailyRate = 1m,
                Make = "Honda",
                Model = "Accord"
            };
        }

        public VehicleDto Build()
        {
            return _vehicleDto;
        }
    }
}
