using System;

namespace RentAVehicle.Domain.Features.DailyRates
{
    public class DailyRateDto
    {
        public Guid DailyRateId { get; set; }
        public decimal Price { get; set; }
        public DateTime Deleted { get; set; }
    }
}
