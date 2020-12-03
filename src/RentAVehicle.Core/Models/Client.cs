using BuildingBlocks.Abstractions;
using RentAVehicle.Core.DomainEvents;
using RentAVehicle.Core.ValueObjects;
using System;

namespace RentAVehicle.Core.Models
{
    public class Client: AggregateRoot
    {
        public Client(ClientName name, Email email)
        {
            Apply(new ClientAdded(Guid.NewGuid(), name, email));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(ClientAdded clientAdded)
        {
            ClientId = clientAdded.ClientId;
            Name = (ClientName)clientAdded.Name;
            Email = (Email)clientAdded.Email;
        }

        public void When(ClientRemoved clientRemoved)
        {
            Deleted = clientRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email))
                throw new Exception("Model invalid");
        }

        public void Remove(DateTime dateTime)
        {
            Apply(new ClientRemoved(dateTime));
        }

        public Guid ClientId { get; private set; }
        public ClientName Name { get; set; }
        public Email Email { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
