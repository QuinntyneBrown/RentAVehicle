using BuildingBlocks.Abstractions;
using GoalSetter.Core.DomainEvents;
using GoalSetter.Core.ValueObjects;
using System;

namespace GoalSetter.Core.Models
{
    public class Client: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public void When(ClientAdded clientAdded)
        {

        }

        public void When(ClientRemoved clientRemoved)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Add(string value)
        {
            Apply(new ClientAdded(value));
        }

        public void Remove(string value)
        {
            Apply(new ClientRemoved(value));
        }

        public Guid ClientId { get; private set; }
        public ClientName Name { get; set; }
        public Email Email { get; set; }
    }
}
