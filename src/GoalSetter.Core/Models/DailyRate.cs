using BuildingBlocks.Abstractions;
using GoalSetter.Core.DomainEvents;
using GoalSetter.Core.ValueObjects;
using System;

namespace GoalSetter.Core.Models
{
    public class DailyRate: AggregateRoot
    {
        public DailyRate(Price price)
        {
            Apply(new DailyRateCreated(Guid.NewGuid(), price));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(DailyRateCreated dailyRateCreated)
        {
            DailyRateId = dailyRateCreated.DailyRateId;
            Price = dailyRateCreated.Price;
        }

        protected override void EnsureValidState()
        {
            if (Price == null)
                throw new Exception("Model invalid. Price value can not be null");
        }

        public Guid DailyRateId { get; private set; }
        public Price Price { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
