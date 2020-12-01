using GoalSetter.Core.Models;
using System;

namespace GoalSetter.Domain.Features.Rentals
{
    public class RentalDto
    {
        public Guid RentalId { get; private set; }
        public Guid VehicleId { get; set; }
        public Guid ClientId { get; set; }
        public DateRange DateRange { get; set; }
        public decimal Total { get; set; }
    }
}
