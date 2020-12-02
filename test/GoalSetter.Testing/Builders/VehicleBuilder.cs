using GoalSetter.Core.Models;

namespace GoalSetter.Testing.Builders
{
    public class VehicleBuilder
    {
        private Vehicle _vehicle;

        public VehicleBuilder(string make, string model, DailyRate dailyRate)
        {
            _vehicle = new Vehicle(make,model,dailyRate.DailyRateId);
        }

        public Vehicle Build()
        {
            return _vehicle;
        }
    }
}
