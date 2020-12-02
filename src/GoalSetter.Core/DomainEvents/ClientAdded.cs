using System;

namespace GoalSetter.Core.DomainEvents
{
    public class ClientAdded
    {
        public ClientAdded(Guid clientId, string name, string email)
        {
            ClientId = clientId;
            Name = name;
            Email = email;
        }
        public Guid ClientId { get; set; }
        public string Name { get; }
        public string Email { get; set; }
    }
}
