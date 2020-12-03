using RentAVehicle.Core.Models;
using RentAVehicle.Core.ValueObjects;
using System;

namespace RentAVehicle.Testing.Builders
{
    public class VehicleBuilder
    {
        private Vehicle _vehicle;

        public VehicleBuilder(int year = 2004, string make = "Honda", string model = "Pilot", DailyRate dailyRate = null)
        {
            dailyRate ??= new DailyRate((Price)1m);

            _vehicle = new Vehicle(year, make,model,dailyRate.DailyRateId);
        }

        public VehicleBuilder Remove()
        {
            _vehicle.Remove(DateTime.UtcNow);

            return this;
        }
        public Vehicle Build()
        {
            return _vehicle;
        }
    }
}
